
using MHW.SaveEditor.Core.Crypto;
using System;
using System.IO;
using System.Security.Cryptography;
using static MHW.SaveEditor.Core.Crypto.Utility;
using static System.Runtime.InteropServices.MemoryMarshal;
using System.Linq;

namespace MHW.SaveEditor.Core
{
  public class SaveFile
  {
    private Cipher Cipher => new Cipher("xieZjoe#P2134-3zmaghgpqoe0z8$3azeq");
    private readonly byte[] data;

    public SaveFileHeader Header => new SaveFileHeader(data.AsSpan(..64));
    public ReadOnlySpan<long> SectionIndex => Cast<byte, long>(data.AsSpan(64..96));
    public Span<byte> Controls => ReadSectionData(0, 0x1AE26B02);
    public Span<byte> Options => ReadSectionData(1, 0x3DB01353);
    public Span<byte> UnknownSection => ReadSectionData(2, 0x748E3ABE);
    public SavesSection SaveSlots => new SavesSection(ReadSectionData(3, SavesSection.SIGNATURE));

    private Span<byte> ReadSectionData(int sectionIndex, uint signature)
      => ReadSectionData(
        data.AsSpan(
          (int)SectionIndex[sectionIndex]
          ..(sectionIndex < SectionIndex.Length - 1 ? (int)SectionIndex[sectionIndex + 1] : ^8)),
        signature
        );

    private Span<byte> ReadSectionData(Span<byte> section, uint? signatureCheck = null)
    {
      var signature = Read<uint>(section[..4]);
      var unknown = Read<uint>(section[4..8]);
      var sectionSize = Read<ulong>(section[8..16]);
      var sectionData = section[16..];
      if (sectionData.Length != (int)sectionSize)
        throw new Exception($"Wrong section length: Expected {sectionSize}, was {sectionData.Length}");
      if (signatureCheck is uint sc && signature != sc)
        throw new Exception($"Incorrect section signature: Expected {sc}, was {signature}");
      return sectionData;
    }

    public SaveFile(byte[] encryptedData)
    {
      data = Cipher.Decipher(encryptedData).ToArray();
    }

    public bool IsDecrypted => Header.Signature == 1;
    public int FileSize => data.Length;

    public Span<byte> GenerateChecksum()
    {
      using var sha = SHA1.Create();
      return FlipEndian(sha.ComputeHash(data[64..].ToArray()));
    }

    public void Save(string path, bool encrypt = true)
    {
      FlipEndian(GenerateChecksum()).CopyTo(Header.DataHash);

      File.WriteAllBytes(path, (encrypt ? Cipher.Encipher(data) : data).ToArray());
    }

    //public byte ResetVouchers
    //{
    //  get => data[0x3000B4];
    //  set => data[0x3000B4] = value;
    //}
  }
}

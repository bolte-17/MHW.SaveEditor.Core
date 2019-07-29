using System;
using static System.Runtime.InteropServices.MemoryMarshal;

namespace MHW.SaveEditor.Core
{
  public ref struct SaveFileHeader
  {
    private readonly Span<byte> data;
    public SaveFileHeader(Span<byte> data)
    {
      if (data.Length != 64)
        throw new ArgumentException($"Expected header of length 64, was {data.Length}", nameof(data));
      this.data = data;
    }
    public ref readonly uint Signature => ref GetReference(Cast<byte, uint>(data[0..4]));
    public Span<byte> DataHash => data[12..32];
    public ref readonly ulong DataSize => ref GetReference(Cast<byte, ulong>(data[32..40]));
    public ref ulong SteamID => ref GetReference(Cast<byte, ulong>(data[40..64]));
  }
}

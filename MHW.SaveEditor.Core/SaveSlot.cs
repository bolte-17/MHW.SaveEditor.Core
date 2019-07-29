using System;
using System.Text;
using static System.Runtime.InteropServices.MemoryMarshal;

namespace MHW.SaveEditor.Core
{
  public ref struct SaveSlot
  {
    public const int SIZE = 0xF6111;
    private readonly Span<byte> data;

    public SaveSlot(Span<byte> data) => this.data = data;

    private readonly Span<byte> characterName => data[..64];
    public string CharacterName {
      get => Encoding.UTF8.GetString(characterName).TrimEnd('\0');
      set
      {
        characterName.Fill(0x00);
        Encoding.UTF8.GetBytes(value).CopyTo(characterName);
      }
    }
    private Span<uint> basicStats => Cast<byte, uint>(data[64..84]);
    public ref uint HunterRank => ref basicStats[0];
    public ref uint Zeni => ref basicStats[1];
    public ref uint ResearchPoints => ref basicStats[2];
    public ref uint HunterXP => ref basicStats[3];
    public ref uint Playtime => ref basicStats[4];

  }
}
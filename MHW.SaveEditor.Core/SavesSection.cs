using System;

namespace MHW.SaveEditor.Core
{
  public readonly ref struct SavesSection
  {
    public const uint SIGNATURE = 0xAD35B985;

    private readonly Span<byte> data;
    public SavesSection(Span<byte> data) => this.data = data;

    public SaveSlot this[int slotNumber] => new SaveSlot(data.Slice(4 + slotNumber * SaveSlot.SIZE, SaveSlot.SIZE));
  }
}
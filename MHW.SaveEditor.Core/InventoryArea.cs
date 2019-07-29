using System;

namespace MHW.SaveEditor.Core
{
  public ref struct InventoryArea
  {
    private Span<byte> data;
    public InventoryArea(Span<byte> data)
    {
      this.data = data;
    }
  }
}
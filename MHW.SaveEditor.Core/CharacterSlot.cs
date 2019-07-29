using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MHW.SaveEditor.Core
{
  public ref struct CharacterSlot
  {
    //Type: 0=Item, 1=Material, 2=Account Item, 3=Ammo/Coating, 4=Decoration
    // Item: 8 bytes
    // 
    public const int BYTES_PER_ITEM = 8;

    public static (int localoffset, int count, int type, string area)[] AreaSet = new[]
    {
        (0xa2c79, 24, 0, "Item Pouch"),
        (0xa2d39, 16, 3, "Ammo Pouch"),
        (0xa2ed9, 200, 0, "Item Box"),
        (0xa3519, 200, 3, "Ammo Box"),
        (0xa3b59, 800, 1, "Material Box"),
        (0xa5459, 200, 4, "Deco Box")
    };

    public enum ItemType
    {
      Item,
      Material,
      AccountItem,
      Ammo,
      Decoration
    }

    public CharacterSlot(Span<byte> data)
    {
      ItemPouch = new InventoryArea(data.Slice(0xa2c79, 24 * BYTES_PER_ITEM));
      AmmoPouch = new InventoryArea(data.Slice(0xa2d39, 16 * BYTES_PER_ITEM));
      ItemBox = new InventoryArea(data.Slice(0xa2ed9, 200 * BYTES_PER_ITEM));
      AmmoBox = new InventoryArea(data.Slice(0xa3519, 200 * BYTES_PER_ITEM));
      MaterialBox = new InventoryArea(data.Slice(0xa3b59, 800 * BYTES_PER_ITEM));
      DecorationBox = new InventoryArea(data.Slice(0xa5459, 200 * BYTES_PER_ITEM));
    }

    public InventoryArea ItemPouch { get; set; }
    public InventoryArea AmmoPouch { get; set; }
    public InventoryArea ItemBox { get; set; }
    public InventoryArea AmmoBox { get; set; }
    public InventoryArea MaterialBox { get; set; }
    public InventoryArea DecorationBox { get; set; }
    //public List<Investigation> Investigations { get; set; }
  }
}

using Content.Shared.Item;
using Content.Shared.Whitelist;
using Robust.Shared.Prototypes;

namespace Content.Shared._Funkystation.Inventory;

[Prototype]
public sealed partial class FunkyInventorySlotPrototype : IPrototype
{
    [ViewVariables]
    [IdDataField]
    public string ID { get; private set; } = default!;

    /// <summary>
    /// The index of where it should be in the sort order.
    /// Items with a lower sort index will come first.
    /// Set this in accordance to other slot sort indices.
    /// </summary>
    ///
    /// <value>
    /// <c>null</c> is reserved for clothes which appear in the dropdown instead
    /// </value>
    [DataField]
    public int? SortIndex;

    /// <summary>
    /// The slot's position in the strip menu and clothes dropdown.
    /// Empty rows are not shown.
    /// If two slots share a position they will be offset one to the right.
    /// </summary>
    ///
    /// <value>
    /// (0, 0) is the top left.
    /// </value>
    [DataField(required: true)]
    public Vector2i StripMenuPosition;

    /// <summary>
    /// The whitelist of what items can fit in a slot.
    /// </summary>
    [DataField]
    public EntityWhitelist? Whitelist;

    [DataField]
    public ProtoId<ItemSizePrototype>? MaxItemSize;

    /// <summary>
    /// Whether to drop the contained item when the inventory is unequipped.
    /// </summary>
    [DataField]
    public bool DropOnUnequip = false;

    /// <summary>
    /// If a hand is hidden in the strip/pickpocket menu.
    /// </summary>
    [DataField]
    public bool IsHiddenInStripMenu = false;

    /// <summary>
    /// If a slot is a hand<br />
    /// • it can be the active hand<br />
    /// • items can be dropped, thrown, etc. from it<br />
    /// • you will not inherit inventory slots from items equipped in it
    /// </summary>
    [DataField]
    public bool IsHand = false;
}

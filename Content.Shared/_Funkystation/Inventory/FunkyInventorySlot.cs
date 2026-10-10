using Content.Shared.Whitelist;

namespace Content.Shared._Funkystation.Inventory;
[DataDefinition]
public sealed partial class FunkyInventorySlot
{
    [DataField]
    public FunkyInventorySlotEnum SlotType { get; private set; }

    [DataField]
    public EntityWhitelist? Whitelist { get; private set; }

    [DataField]
    public bool Drop { get; private set; } = false;
}

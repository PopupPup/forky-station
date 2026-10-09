using Content.Shared.Whitelist;

namespace Content.Server._Funkystation.Inventory;

[DataDefinition]
public sealed partial class FunkyInventorySlot
{
    public readonly FunkyInventorySlotType SlotType;
    public readonly EntityWhitelist? Whitelist;
}

using Robust.Shared.Prototypes;

namespace Content.Server._Funkystation.Inventory;

[Prototype]
public sealed partial class FunkyInventoryTemplatePrototype: IPrototype
{
    [IdDataField] public string ID { get; private set; } = string.Empty;
    [DataField] public readonly List<FunkyInventorySlot> Slots;
}

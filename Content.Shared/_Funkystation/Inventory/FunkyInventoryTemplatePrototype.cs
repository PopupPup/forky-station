using Robust.Shared.Prototypes;

namespace Content.Shared._Funkystation.Inventory;

[Prototype]
public sealed partial class FunkyInventoryTemplatePrototype : IPrototype
{
    [IdDataField]
    public string ID { get; private set; } = string.Empty;

    [DataField]
    public List<FunkyInventorySlot> Slots { get; private set; }
}

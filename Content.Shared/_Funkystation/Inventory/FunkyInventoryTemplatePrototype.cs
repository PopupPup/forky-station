using Robust.Shared.Prototypes;

namespace Content.Shared._Funkystation.Inventory;

[Prototype]
public sealed partial class FunkyInventoryTemplatePrototype : IPrototype
{
    [ViewVariables]
    [IdDataField]
    public string ID { get; private set; } = default!;

    /// <summary>
    /// Whether being equipped should cause its slots to be inherited by the equipper.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> for items like clothing<br/>
    /// <c>false</c> for items like mice
    /// </value>
    [DataField(required: true)]
    public bool Inheritable;

    /// <summary>
    /// The slots in this template
    /// </summary>
    [DataField(required: true)]
    public ProtoId<FunkyInventorySlotPrototype>[] Slots;
}

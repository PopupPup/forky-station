namespace Content.Server._Funkystation.Inventory;

public sealed class FunkyInventoryTemplate(FunkyInventoryTemplatePrototype prototype)
{
    public readonly FunkyInventoryTemplatePrototype Prototype = prototype;
    public readonly Guid Guid = Guid.NewGuid();
}

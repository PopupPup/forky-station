namespace Content.Server._Funkystation.Objectives.Components;

/// <summary>
/// Modifies the price of an entity for scorethief, including possibly making it worthless
/// </summary>
public sealed partial class ScoreThiefPriceModifierComponent : Component
{
    [DataField(required: true)]
    public string Reason;

    [DataField(required: true)]
    public string Multiplier;
}

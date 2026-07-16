using Content.Server._Funkystation.Objectives.Systems;
using Robust.Shared.Serialization.TypeSerializers.Implementations;

namespace Content.Server._Funkystation.Objectives.Components;

/// <summary>
/// Modifies the price of an entity for scorethief, including possibly making it worthless
/// </summary>
[RegisterComponent, Access(typeof(ScoreThiefConditionSystem))]
public sealed partial class ScoreThiefPriceModifierComponent : Component
{
    /// <summary>
    /// Must be a Loc string
    /// </summary>
    [DataField(required: true)]
    public string Reason;

    [DataField(required: true)]
    public double Multiplier;
}

using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._Funkystation.Inventory;

[RegisterComponent, NetworkedComponent, /*AutoGenerateComponentState(true)*/]
public sealed partial class FunkyInventoryComponent : Component
{
    /// <summary>
    /// The template of the slots this entity has.<br/>
    /// Changing this value after init is <i><b>not</b></i> supported.
    /// </summary>
    public ProtoId<FunkyInventoryTemplatePrototype> Template = "baseBiped";


}

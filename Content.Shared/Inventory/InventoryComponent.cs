using Content.Shared.DisplacementMap;
using Robust.Shared.Containers;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared.Inventory;

[RegisterComponent, NetworkedComponent]
[Access(typeof(InventorySystem))]
[AutoGenerateComponentState(true, true)] // FUNKY CHANGE
public sealed partial class InventoryComponent : Component
{

    /// <summary>
    /// The templates defining how the inventory layout will look like.
    /// </summary>
    [DataField, AutoNetworkedField]
    [ViewVariables] // use the API method
    public ProtoId<InventoryTemplatePrototype>[] TemplateId = ["human"]; // FUNKY CHANGE

    // FUNKY: god. GOD. I need to rewrite the whole damn inventory system at this point. Damnit. It works. At least. #killeveryone
    [AutoNetworkedField] // FUNKY CHANGE
    public List<EntityUid> Owners = new List<EntityUid>([EntityUid.Invalid]); // FUNKY CHANGE.
    // Invalid = it's owned by the player. We do this because nullable EntityUid collections aren't automatically networked :P


    /// <summary>
    /// For setting the TemplateId.
    /// </summary>
    /* FUNKY CHANGE
     [ViewVariables(VVAccess.ReadWrite)]
    public ProtoId<InventoryTemplatePrototype> TemplateIdVV
    {
        get => TemplateId;
        set => IoCManager.Resolve<IEntityManager>().System<InventorySystem>().SetTemplateId((Owner, this), value);
    }*/

    [DataField, AutoNetworkedField]
    public string? SpeciesId;


    [ViewVariables]
    public SlotDefinition[] Slots = [];

    [ViewVariables]
    public ContainerSlot[] Containers = [];

    [DataField, AutoNetworkedField]
    public Dictionary<string, DisplacementData> Displacements = new();

    /// <summary>
    /// Alternate displacement maps, which if available, will be selected for the player of the appropriate gender.
    /// </summary>
    [DataField, AutoNetworkedField]
    public Dictionary<string, DisplacementData> FemaleDisplacements = new();

    /// <summary>
    /// Alternate displacement maps, which if available, will be selected for the player of the appropriate gender.
    /// </summary>
    [DataField, AutoNetworkedField]
    public Dictionary<string, DisplacementData> MaleDisplacements = new();
}

/// <summary>
/// Raised if the <see cref="InventoryComponent.TemplateId"/> of an inventory changed.
/// </summary>
[ByRefEvent]
public struct InventoryTemplateUpdated;

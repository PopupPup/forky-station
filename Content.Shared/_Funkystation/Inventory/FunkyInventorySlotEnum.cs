namespace Content.Shared._Funkystation.Inventory;
/// <summary>
/// <para> The type of an inventory (hotbar) slot. </para>
/// <para> Specifies behavior like location on the HUD as well as human-readable naming for the storage.</para>
/// </summary>
[Flags, Serializable]
public enum FunkyInventorySlotEnum
{
    Head =          0b0000_0000_0000_0001,
    Eyes =          0b0000_0000_0000_0010,
    Ears =          0b0000_0000_0000_0100,
    Mask =          0b0000_0000_0000_1000,
    OuterClothing = 0b0000_0000_0001_0000,
    InnerClothing = 0b0000_0000_0010_0000,
    Neck =          0b0000_0000_0100_0000,
    Back =          0b0000_0000_1000_0000,
    Belt =          0b0000_0001_0000_0000,
    Gloves =        0b0000_0010_0000_0000,
    Pocket =        0b0000_0100_0000_0000,
    Feet =          0b0000_1000_0000_0000,
    SuitStorage =   0b0001_0000_0000_0000,
    Weapon =        0b0010_0000_0000_0000, // Funky slot type. If you want to disable this, find where it's used in YAML and remove it there. Do NOT comment it out here.
}

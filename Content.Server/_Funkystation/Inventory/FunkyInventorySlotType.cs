namespace Content.Server._Funkystation.Inventory;

[Flags, Serializable]
public enum FunkyInventorySlotType
{
    Head =          0b0000_0000_0001,
    Eyes =          0b0000_0000_0010,
    Ears =          0b0000_0000_0100,
    Mask =          0b0000_0000_1000,
    OuterClothing = 0b0000_0001_0000,
    InnerClothing = 0b0000_0010_0000,
    Neck =          0b0000_0100_0000,
    Back =          0b0000_1000_0000,
    Belt =          0b0001_0000_0000,
    Gloves =        0b0010_0000_0000,
    Pocket =        0b0100_0000_0000,
    Feet =          0b1000_0000_0000,
}

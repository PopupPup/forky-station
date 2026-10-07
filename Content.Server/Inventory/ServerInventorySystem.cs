using Content.Shared.Explosion;
using Content.Shared.Inventory;
using Content.Shared.Inventory.Events;
using Robust.Shared.Prototypes;

namespace Content.Server.Inventory
{
    public sealed partial class ServerInventorySystem : InventorySystem
    {
        public override void Initialize()
        {
            base.Initialize();

            SubscribeLocalEvent<InventoryComponent, BeforeExplodeEvent>(OnExploded);
            SubscribeLocalEvent<InventoryComponent, EquipableInventoryChangeEvent>(EquipableInventoryChange); // FUNKY CHANGE
        }

        private void EquipableInventoryChange(Entity<InventoryComponent> ent, ref EquipableInventoryChangeEvent args)
        {
            if (args.Add)
            {
                Array.Resize(ref ent.Comp.TemplateId, ent.Comp.TemplateId.Length + 1);
                Array.Resize(ref ent.Comp.Owners, ent.Comp.Owners.Length + 1);
                ent.Comp.TemplateId[^1] = args.Inventory.TemplateId;
                ent.Comp.Owners[^1] = args.Inventory.Owner;
            }
            else
            {
                var newTemplateId = new List<ProtoId<InventoryTemplatePrototype>>();
                var newOwners = new List<EntityUid?>();
                var removed = false;
                for (var i = 0; i < ent.Comp.TemplateId.Length; i++)
                {
                    if (ent.Comp.TemplateId[i] != args.Inventory.TemplateId || removed)
                    {
                        newTemplateId.Add(ent.Comp.TemplateId[i]);
                        newOwners.Add(ent.Comp.Owners[i]);
                    }
                    else
                    {
                        removed = true;
                    }

                    ent.Comp.TemplateId = newTemplateId.ToArray();
                    ent.Comp.Owners = newOwners.ToArray();
                }
            }
            DirtyField(ent, ent.Comp, nameof(InventoryComponent.TemplateId));
            UpdateInventoryTemplate(ent);
        }

        private void OnExploded(Entity<InventoryComponent> ent, ref BeforeExplodeEvent args)
        {
            // explode each item in their inventory too
            var slots = new InventorySlotEnumerator(ent);
            while (slots.MoveNext(out var slot))
            {
                if (slot.ContainedEntity != null)
                    args.Contents.Add(slot.ContainedEntity.Value);
            }
        }

        public void TransferEntityInventories(Entity<InventoryComponent?> source, Entity<InventoryComponent?> target)
        {
            if (!Resolve(source.Owner, ref source.Comp) || !Resolve(target.Owner, ref target.Comp))
                return;

            var enumerator = new InventorySlotEnumerator(source.Comp);
            while (enumerator.NextItem(out var item, out var slot))
            {
                if (TryUnequip(source, slot.Name, true, true, inventory: source.Comp, triggerHandContact: true))
                    TryEquip(target, item, slot.Name , true, true, inventory: target.Comp, triggerHandContact: true);
            }
        }
    }
}

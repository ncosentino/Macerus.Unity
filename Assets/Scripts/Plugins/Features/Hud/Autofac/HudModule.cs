using Assets.Scripts.Plugins.Features.Hud.Equipment;
using Assets.Scripts.Plugins.Features.Hud.Inventory;
using Assets.Scripts.Unity.Resources;

using Autofac;

namespace Assets.Scripts.Scenes.Explore.Autofac
{
    public sealed class HudModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<IconEquipmentSlotBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DropEquipmentSlotBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DragEquipmentItemBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EquipmentSlotsFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ItemListFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ItemToListItemEntryConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<InventoryListItemColorMutator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<InventoryListItemNameMutator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<InventoryListItemIconMutator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<InventoryListItemEquippableMutator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ItemListBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DragItemFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DropInventoryBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DragInventoryListItemBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterBuildCallback(c =>
                {
                    var registrar = c.Resolve<IPrefabCreatorRegistrar>();
                    registrar.Register<IInventoryListItemPrefab>(obj => new InventoryListItemPrefab(obj));
                    registrar.Register<IItemListPrefab>(obj => new ItemListPrefab(obj));
                    registrar.Register<IDragItemPrefab>(obj => new DragItemPrefab(obj));
                    registrar.Register<IEquipSlotPrefab>(obj => new EquipSlotPrefab(obj));
                });
        }
    }
}

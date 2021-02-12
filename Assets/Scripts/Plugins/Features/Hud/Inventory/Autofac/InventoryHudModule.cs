using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.Resources;

using Autofac;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Autofac
{
    public sealed class InventoryHudModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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
                });
        }
    }
}

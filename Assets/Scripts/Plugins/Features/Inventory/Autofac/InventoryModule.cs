using Assets.Scripts.Plugins.Features.Inventory;
using Assets.Scripts.Plugins.Features.Inventory.Noesis;
using Assets.Scripts.Plugins.Features.Inventory.Noesis.Resources;

using Autofac;

namespace Assets.Scripts.Plugins.Features.Inventory.Autofac
{
    public sealed class InventoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder
            //    .RegisterType<PlayerInventoryWindow>()
            //    .AsImplementedInterfaces()
            //    .SingleInstance();
            //builder
            //    .RegisterType<InventoryBagView>()
            //    .AsImplementedInterfaces()
            //    .SingleInstance();
            //builder
            //    .RegisterType<InventoryEquipmentView>()
            //    .AsImplementedInterfaces()
            //    .SingleInstance();
        }
    }
}

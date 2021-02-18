using Assets.Scripts.Plugins.Features.Hud.Equipment;
using Assets.Scripts.Plugins.Features.Hud.Equipment.Api;
using Assets.Scripts.Unity.Resources.Prefabs;

using Autofac;

namespace Assets.Scripts.Plugins.Features.Hud.Autofac
{
    public sealed class EquipmentHudModule : Module
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
                .RegisterType<EquipmentItemDropUiFlow>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterBuildCallback(c =>
                {
                    var registrar = c.Resolve<IPrefabCreatorRegistrar>();
                    registrar.Register<IEquipSlotPrefab>(obj => new EquipSlotPrefab(obj));
                });
        }
    }
}

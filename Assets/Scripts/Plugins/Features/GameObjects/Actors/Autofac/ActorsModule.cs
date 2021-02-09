using Assets.Scripts.Api.GameObjects;
using Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors;
using Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours;
using Assets.Scripts.Plugins.Features.Wip;
using Autofac;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Autofac
{
    public sealed class ActorsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<AdditionalActorBehaviorsProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();

            // player
            builder
                .RegisterType<PlayerInputControlsBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PlayerPrefabStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .AsSelf()
                .AutoActivate()
                .OnActivated(x =>
                {
                    x.Context.Resolve<IPrefabStitcherRegistrar>().Register(
                        @"Mapping/Prefabs/PlayerPlaceholder",
                        x.Instance.Stitch);
                });

            builder
                .RegisterType<HasGuiEquipmentBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasGuiEquipmentBehaviorInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .AsSelf()
                .AutoActivate();
            builder
                .RegisterType<HasGuiInventoryBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasGuiInventoryBehaviorInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .AsSelf()
                .AutoActivate();
            builder
                .RegisterType<WipInventoryInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .AsSelf()
                .AutoActivate();
            builder
                .RegisterType<LightRadiusBehaviorInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .AsSelf()
                .AutoActivate();

            builder
                .RegisterType<HasFollowCameraBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasFollowCameraBehaviorInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .AsSelf()
                .AutoActivate();
        }
    }
}
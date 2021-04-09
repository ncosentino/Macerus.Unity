using Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors;
using Assets.Scripts.Plugins.Features.GameObjects.Actors.Player;

using Autofac;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Autofac
{
    public sealed class ActorsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<ActorBehaviorsProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<LightRadiusBehaviorInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<LightRadiusStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasFollowCameraBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasFollowCameraBehaviorInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorsCantBePushedInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DrawActorWalkPathInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DrawWalkPathBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();

            // player
            builder
                .RegisterType<PlayerQuickSlotControlsBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PlayerInteractionControlsBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PlayerMovementControlsBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PlayerPrefabStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterType<HasGuiEquipmentBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasGuiEquipmentBehaviorInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasGuiInventoryBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasGuiInventoryBehaviorInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
using Autofac;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common.Autofac
{
    public sealed class GameObjectsCommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<UnityGameObjectRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<IdentityBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SyncMacerusToUnityVelocityBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SyncUnityToMacerusPositionBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SyncMacerusToUnityPositionBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MacerusToUnityPositionSynchronizer>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MovementBehaviorInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasGameObjectBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PrefabStitcherFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<GameObjectBehaviorInterceptorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

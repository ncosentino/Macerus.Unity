using Assets.Scripts.Scenes.Explore.Api;
using Assets.Scripts.Scenes.Explore.Camera;
using Assets.Scripts.Scenes.Explore.Input;

using Autofac;

namespace Assets.Scripts.Scenes.Explore.Autofac
{
    public sealed class ExploreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ExploreSceneStartupInterceptorFacade>()
                .As<IExploreSceneStartupInterceptorFacade>() // force facade interface to avoid circular dependencies
                .SingleInstance();
            builder
                .RegisterType<ExploreSceneLoadHook>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ExploreSetup>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<GameEngineUpdateBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            
            RegisterCamera(builder);
            RegisterInput(builder);
        }

        private static void RegisterInput(ContainerBuilder builder)
        {
            builder
                .RegisterType<GuiInputController>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<KeyboardControls>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<GuiInputStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private static void RegisterCamera(ContainerBuilder builder)
        {
            builder
                .RegisterType<AutoTargetFollowCameraFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CameraAutoTargetBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

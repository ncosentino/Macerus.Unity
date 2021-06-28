using Autofac;

namespace Assets.Scripts.Plugins.Features.Cameras.Autofac
{
    public sealed class CamerasModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<AutoTargetFollowCameraFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CameraAutoTargetBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasFollowCameraBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

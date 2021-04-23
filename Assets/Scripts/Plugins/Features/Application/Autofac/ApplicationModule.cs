using Autofac;

namespace Assets.Scripts.Plugins.Features.Application.Autofac
{
    public sealed class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<UnityApplication>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SceneManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
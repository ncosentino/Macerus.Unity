using Assets.Scripts.Scenes.LoadingScreen;

using Autofac;

namespace Assets.Scripts.Scenes.LoadingScreen.Autofac
{
    public sealed class LoadingScreenModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<LoadingScreenLoadHook>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<LoadingScreenSetup>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

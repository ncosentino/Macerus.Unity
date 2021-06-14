using Assets.Scripts.Scenes.LoadingScreen.Gui.Noesis;
using Assets.Scripts.Scenes.LoadingScreen.Gui.Noesis.Resources;

using Autofac;

namespace Assets.Scripts.Scenes.LoadingScreen.Gui.Autofac
{
    public sealed class GuiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<LoadingScreenView>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<LoadingScreenNoesisViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

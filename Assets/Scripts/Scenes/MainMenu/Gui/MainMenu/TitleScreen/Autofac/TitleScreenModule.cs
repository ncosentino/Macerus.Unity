using Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.TitleScreen.Noesis;
using Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.TitleScreen.Noesis.Resources;

using Autofac;

namespace Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.TitleScreen.Autofac
{
    public sealed class TitleScreenModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<TitleScreenView>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TitleScreenNoesisViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

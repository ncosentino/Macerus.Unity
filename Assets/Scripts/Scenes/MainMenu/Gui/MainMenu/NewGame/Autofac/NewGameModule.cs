using Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.NewGame.Noesis;
using Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.NewGame.Noesis.Resources;

using Autofac;

namespace Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.NewGame.Autofac
{
    public sealed class NewGameModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<NewGameView>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NewGameNoesisViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

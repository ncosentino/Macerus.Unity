using Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.Noesis.Resources;

using Autofac;

namespace Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.Autofac
{
    public sealed class MainMenuModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MainMenuView>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

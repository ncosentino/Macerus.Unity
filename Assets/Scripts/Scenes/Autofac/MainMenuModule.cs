using Assets.Scripts.Scenes.MainMenu.Input;
using Autofac;

namespace Assets.Scripts.Autofac.Scenes
{
    public sealed class MainMenuModule : Module
    {
        protected override void Load(ContainerBuilder builder)
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
    }
}

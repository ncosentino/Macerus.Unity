using Autofac;

namespace Assets.Scripts.Scenes.Explore.Gui.Autofac
{
    public sealed class ProvidedImplementationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<GuiCanvasProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

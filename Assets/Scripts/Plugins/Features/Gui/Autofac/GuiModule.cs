using Autofac;

namespace Assets.Scripts.Plugins.Features.Gui.Autofac
{
    public sealed class GuiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ModalContentPresenter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ModalWindow>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StringModalContentConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

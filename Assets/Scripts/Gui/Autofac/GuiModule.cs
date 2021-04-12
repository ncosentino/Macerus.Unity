using Autofac;

namespace Assets.Scripts.Gui
{
    public sealed class GuiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<GuiHitTesterFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

using Autofac;

namespace Assets.Scripts.Gui.Noesis.Autofac
{
    public sealed class NoesisGuiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<LateBindNoesisGuiBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NoesisGuiHitTester>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

using Autofac;

namespace Assets.Scripts.Gui.Unity.Autofac
{
    public sealed class UnityGuiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<UnityGuiHitTester>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

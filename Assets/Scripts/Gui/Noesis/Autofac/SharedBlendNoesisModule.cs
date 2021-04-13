using Autofac;

namespace Assets.Scripts.Gui.Noesis.Autofac
{
    public sealed class SharedBlendNoesisModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<ResourceImageSourceFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

using Autofac;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static.Autofac
{
    public sealed class StaticObjectsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<StaticGameObjectBehaviorsProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
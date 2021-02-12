using Autofac;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static.Autofac
{
    public sealed class TriggersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<OnEnterTriggerScriptBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<OnEnterTriggerScriptInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<OnExitTriggerScriptBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<OnExitTriggerScriptInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
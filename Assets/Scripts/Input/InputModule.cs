using Assets.Scripts.Input;

using Autofac;

namespace Assets.Scripts.Autofac.Unity
{
    public sealed class InputModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<KeyboardControls>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

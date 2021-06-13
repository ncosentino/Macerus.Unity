using Assets.Scripts.Plugins.Features.SceneTransitions.Noesis.Resources;

using Autofac;

namespace Assets.Scripts.Plugins.Features.SceneTransitions.Autofac
{
    public sealed class SceneTransitionsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<FaderSceneTransitionView>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

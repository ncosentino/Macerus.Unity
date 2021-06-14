using Assets.Scripts.Plugins.Features.SceneTransitions.Noesis;
using Assets.Scripts.Plugins.Features.SceneTransitions.Noesis.Resources;

using Autofac;

namespace Assets.Scripts.Plugins.Features.SceneTransitions.Autofac
{
    public sealed class SceneTransitionsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<FaderSceneTransitionNoesisViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<FaderSceneTransitionView>()
                .AsImplementedInterfaces();
                //.SingleInstance(); // NOTE: we should allow multiple instances of this w/ shared view model
        }
    }
}


using Assets.Scripts.Plugins.Features.Animations.Interceptors;
using Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours;

using Autofac;

namespace Assets.Scripts.Plugins.Features.Animations.Autofac
{
    public sealed class AnimationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<SpriteAnimationProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SpriteAnimationBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SpriteAnimationBehaviorInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .AsSelf()
                .AutoActivate();
        }
    }
}

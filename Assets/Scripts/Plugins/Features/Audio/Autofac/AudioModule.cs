using Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors;
using Assets.Scripts.Plugins.Features.GameObjects.Actors.Player;

using Autofac;

namespace Assets.Scripts.Plugins.Features.Audio
{
    public sealed class AudioModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<SoundPlayingBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
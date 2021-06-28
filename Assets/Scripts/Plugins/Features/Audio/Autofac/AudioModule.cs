using System;

using Assets.Scripts.Behaviours.Audio;
using Assets.Scripts.Unity.Resources;

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
            builder
                .Register(c =>
                {
                    var audioManager = new AudioManager(
                        new Lazy<IUnityAudioManager>(() => AudioManagerBehaviour.Instance),
                        c.Resolve<IResourceLoader>());
                    return audioManager;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
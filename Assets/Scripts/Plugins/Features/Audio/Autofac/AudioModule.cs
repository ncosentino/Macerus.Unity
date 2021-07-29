using System;

using Assets.Scripts.Behaviours.Audio;
using Assets.Scripts.Unity.Resources;

using Autofac;

using Macerus.Plugins.Features.Audio.SoundGeneration;

using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Assets.Scripts.Plugins.Features.Audio
{
    public sealed class AudioModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c =>
                {
                    var audioManager = new AudioManager(
                        new Lazy<IUnityAudioManager>(() => AudioManagerBehaviour.Instance),
                        c.Resolve<Lazy<ISoundGenerator>>(),
                        c.Resolve<Lazy<IFilterContextProvider>>(),
                        c.Resolve<IResourceLoader>());
                    return audioManager;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
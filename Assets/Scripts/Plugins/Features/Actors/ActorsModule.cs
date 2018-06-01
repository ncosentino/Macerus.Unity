using Assets.Scripts.Api.GameObjects;
using Assets.Scripts.Plugins.Features.Actors.UnityBehaviours;
using Autofac;

namespace Assets.Scripts.Plugins.Features.Actors
{
    public sealed class ActorsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<AdditionalActorBehaviorsProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();

            // player
            builder
                .RegisterType<PlayerInputControlsBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PlayerPrefabStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .AsSelf()
                .AutoActivate()
                .OnActivated(x =>
                {
                    x.Context.Resolve<IPrefabStitcherRegistrar>().Register(
                        @"Mapping/Prefabs/PlayerPlaceholder",
                        x.Instance.Stitch);
                });
        }
    }
}
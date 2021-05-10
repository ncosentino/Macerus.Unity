using Assets.ContentCreator.MapEditor.Behaviours;
using Assets.Scripts.Scenes.Explore.Api;

using Autofac;

namespace Assets.ContentCreator.MapEditor.Autofac
{
    public sealed class MapEditorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ExploreSceneStartupInterceptorFacade>()
                .As<IExploreSceneStartupInterceptorFacade>() // force facade interface to avoid circular dependencies
                .SingleInstance();
            builder
                .RegisterType<ExploreSceneLoadHook>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ExploreSetup>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<GameEngineUpdateBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ExploreGameRootPrefabFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterType<DynamicAnimationBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PrefabResourceIdConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TemplateIdentifierBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TypeIdentifierBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<IdentifierBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WorldLocationBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SceneToMapConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<BehaviorConverterFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

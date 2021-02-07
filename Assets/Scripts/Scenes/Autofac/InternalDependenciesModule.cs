using System.Collections.Generic;

using Assets.Scripts.Scenes.Api;

using Autofac;

using ProjectXyz.Api.Logging;

namespace Assets.Scripts.Scenes.Autofac
{
    public sealed class InternalDependenciesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterBuildCallback(x =>
                {
                    var logger = x.Resolve<ILogger>();
                    foreach (var sceneLoadHook in x.Resolve<IEnumerable<IDiscoverableSceneLoadHook>>())
                    {
                        logger.Debug($"Created scene load hook '{sceneLoadHook}'.");
                        sceneLoadHook.SwitchScene();
                    }
                });
        }
    }
}

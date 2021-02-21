using Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors;
using Assets.Scripts.Plugins.Features.GameObjects.Containers;
using Assets.Scripts.Unity.Resources.Prefabs;

using Autofac;

namespace Assets.Scripts.Plugins.Features.Wip
{
    public sealed class WipModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<TestInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

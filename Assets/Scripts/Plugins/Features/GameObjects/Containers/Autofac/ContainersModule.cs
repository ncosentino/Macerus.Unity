using Assets.Scripts.Unity.Resources.Prefabs;

using Autofac;

namespace Assets.Scripts.Plugins.Features.GameObjects.Containers.Autofac
{
    public sealed class ContainersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterBuildCallback(c =>
                {
                    var registrar = c.Resolve<IPrefabCreatorRegistrar>();
                    registrar.Register<IContainerPrefab>(obj => new ContainerPrefab(obj));
                });
        }
    }
}

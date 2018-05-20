using Assets.Scripts.Maps;
using Autofac;

namespace Assets.Scripts.Autofac
{
    public sealed class MapModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MapLoader>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

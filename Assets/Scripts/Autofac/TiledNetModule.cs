using Autofac;
using Tiled.Net.Tmx.Xml;

namespace Assets.Scripts.Autofac
{
    public sealed class TiledNetModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<XmlTmxMapParser>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

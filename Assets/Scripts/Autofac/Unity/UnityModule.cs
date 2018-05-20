using Assets.Scripts.Unity;
using Autofac;

namespace Assets.Scripts.Autofac.Unity
{
    public sealed class UnityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<AssetPaths>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

using Assets.Scripts.Unity;
﻿using Assets.Scripts.Behaviours.Threading;
using Assets.Scripts.Unity.Threading;
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
            builder
                .Register(x => new Dispatcher(() => DispatcherBehaviour.Instance))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

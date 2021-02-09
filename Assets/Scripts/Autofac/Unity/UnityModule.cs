﻿using Assets.Scripts.Behaviours.Threading;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.GameObjects;
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
                .RegisterType<TimeProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<GameObjectManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ObjectDestroyer>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(x => new Dispatcher(() => DispatcherBehaviour.Instance))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

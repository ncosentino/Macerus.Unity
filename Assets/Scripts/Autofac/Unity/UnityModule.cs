﻿using System;

using Assets.Scripts.Behaviours.Threading;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;
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
                .RegisterType<UnityGameObjectManager>()
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
            builder
               .Register(x => new CoroutineRunner(
                   new Lazy<ICoroutineRunner>(() => CoroutineRunnerBehaviour.Instance),
                   x.Resolve<Lazy<IDispatcher>>()))
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
                .RegisterType<MouseInput>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<KeyboardInput>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

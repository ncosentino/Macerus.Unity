﻿using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Api.GameObjects;
using Assets.Scripts.Gui.Hud.Inventory;
using Assets.Scripts.Scenes.Explore.Camera;
using Assets.Scripts.Scenes.Explore.GameObjects;
using Assets.Scripts.Scenes.Explore.Input;
using Assets.Scripts.Scenes.Explore.Maps;
using Autofac;

namespace Assets.Scripts.Autofac.Scenes
{
    public sealed class ExploreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<GuiInputController>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<KeyboardControls>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<GuiInputStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<UnityGameObjectRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<IdentityBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapObjectStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WorldLocationStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasGameObjectBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PrefabStitcherFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<GameObjectBehaviorInterceptorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .OnActivated(x =>
                {
                    var interceptors = x.Context
                     .Resolve<IEnumerable<IGameObjectBehaviorInterceptor>>()
                     .Where(interceptor => interceptor != x.Instance);
                    foreach (var interceptor in interceptors)
                    {
                        x.Instance.Register(interceptor);
                    }
                });

            // maps
            builder
                .RegisterType<MapFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TileLoader>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TilesetSpriteResourceResolver>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ExploreMapFormatter>()
                .AsImplementedInterfaces()
                .SingleInstance();

            // camera
            builder
                .RegisterType<AutoTargetFollowCameraFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CameraAutoTargetBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();

            // hud
            builder
                .RegisterType<ItemListFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ItemToListItemEntryConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ItemListBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

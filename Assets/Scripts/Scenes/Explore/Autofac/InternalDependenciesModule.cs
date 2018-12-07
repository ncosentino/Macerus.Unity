using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Scenes.Explore.Api;
using Assets.Scripts.Scenes.Explore.Camera;
using Assets.Scripts.Scenes.Explore.GameObjects;
using Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment;
using Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory;
using Assets.Scripts.Scenes.Explore.Input;
using Assets.Scripts.Scenes.Explore.Maps;
using Autofac;

namespace Assets.Scripts.Scenes.Explore.Autofac
{
    public sealed class InternalDependenciesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ExploreSceneStartupInterceptorFacade>()
                .As<IExploreSceneStartupInterceptorFacade>() // force facade interface to avoid circular dependencies
                .SingleInstance();
            builder
                .RegisterType<ExploreSceneLoadHook>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ExploreSetup>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<GameEngineUpdateBehaviourStitcher>()
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

            RegisterMaps(builder);
            RegisterCamera(builder);
            RegisterHud(builder);
            RegisterInput(builder);
        }

        private static void RegisterInput(ContainerBuilder builder)
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
        }

        private static void RegisterHud(ContainerBuilder builder)
        {
            builder
                .RegisterType<DropEquipmentSlotBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EquipmentSlotsFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ItemListFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ItemToListItemEntryConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<InventoryListItemColorMutator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<InventoryListItemNameMutator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ItemListBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DragInventoryListItemBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private static void RegisterCamera(ContainerBuilder builder)
        {
            builder
                .RegisterType<AutoTargetFollowCameraFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CameraAutoTargetBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private static void RegisterMaps(ContainerBuilder builder)
        {
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
        }
    }
}

using System;
using System.Linq;

using Assets.Scripts.Gui;
using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Gui.Noesis.Views.Resources;
using Assets.Scripts.Gui.Unity;
using Assets.Scripts.Plugins.Features.Audio.Api;
using Assets.Scripts.Plugins.Features.Console;
using Assets.Scripts.Plugins.Features.GameEngine;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Plugins.Features.NewHud.Noesis;
using Assets.Scripts.Scenes.Explore.Api;
using Assets.Scripts.Scenes.Explore.Camera;
using Assets.Scripts.Scenes.Explore.Console;
using Assets.Scripts.Scenes.Explore.Input;
using Assets.Scripts.Unity.GameObjects;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.GameObjects.Actors.Generation;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Scenes.Explore
{
    public sealed class ExploreSetup : IExploreSetup
    {
        private readonly IUnityGameObjectManager _gameObjectManager;
        private readonly IMapPrefabFactory _mapPrefabFactory;
        private readonly IGuiInputStitcher _guiInputStitcher;
        private readonly IExploreSceneStartupInterceptorFacade _exploreSceneStartupInterceptorFacade;
        private readonly IGameEngineUpdateBehaviourStitcher _gameEngineUpdateBehaviourStitcher;
        private readonly IMapManager _mapManager;
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly IGameObjectIdentifiers _gameObjectIdentifiers;
        private readonly IActorIdentifiers _actorIdentifiers;
        private readonly IActorGeneratorFacade _actorGeneratorFacade;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IGuiBehaviourStitcher _guiBehaviorStitcher;
        private readonly ISoundPlayingBehaviourStitcher _soundPlayingBehaviourStitcher;
        private readonly IUnityGuiHitTester _unityGuiHitTester;
        private readonly INoesisGuiHitTester _noesisGuiHitTester;
        private readonly IExploreGameRootPrefabFactory _exploreGameRootPrefabFactory;
        private readonly IHasFollowCameraBehaviourStitcher _hasFollowCameraBehaviourStitcher;
        private readonly Lazy<IHudView> _lazyHudView;

        public ExploreSetup(
            IUnityGameObjectManager gameObjectManager,
            IMapPrefabFactory mapPrefabFactory,
            IGuiInputStitcher guiInputStitcher,
            IExploreSceneStartupInterceptorFacade exploreSceneStartupInterceptorFacade,
            IGameEngineUpdateBehaviourStitcher gameEngineUpdateBehaviourStitcher,
            IMapManager mapManager,
            IMapGameObjectManager mapGameObjectManager,
            IGameObjectIdentifiers gameObjectIdentifiers,
            IActorIdentifiers actorIdentifiers,
            IActorGeneratorFacade actorGeneratorFacade,
            IFilterContextAmenity filterContextAmenity,
            IGuiBehaviourStitcher guiBehaviourStitcher,
            ISoundPlayingBehaviourStitcher soundPlayingBehaviourStitcher,
            IUnityGuiHitTester unityGuiHitTester,
            INoesisGuiHitTester noesisGuiHitTester,
            IExploreGameRootPrefabFactory exploreGameRootPrefabFactory,
            IHasFollowCameraBehaviourStitcher hasFollowCameraBehaviourStitcher,
            Lazy<IHudView> lazyHudView)
        {
            _gameObjectManager = gameObjectManager;
            _mapPrefabFactory = mapPrefabFactory;
            _guiInputStitcher = guiInputStitcher;
            _exploreSceneStartupInterceptorFacade = exploreSceneStartupInterceptorFacade;
            _gameEngineUpdateBehaviourStitcher = gameEngineUpdateBehaviourStitcher;
            _mapManager = mapManager;
            _mapGameObjectManager = mapGameObjectManager;
            _gameObjectIdentifiers = gameObjectIdentifiers;
            _actorIdentifiers = actorIdentifiers;
            _actorGeneratorFacade = actorGeneratorFacade;
            _filterContextAmenity = filterContextAmenity;
            _guiBehaviorStitcher = guiBehaviourStitcher;
            _soundPlayingBehaviourStitcher = soundPlayingBehaviourStitcher;
            _unityGuiHitTester = unityGuiHitTester;
            _noesisGuiHitTester = noesisGuiHitTester;
            _exploreGameRootPrefabFactory = exploreGameRootPrefabFactory;
            _hasFollowCameraBehaviourStitcher = hasFollowCameraBehaviourStitcher;
            _lazyHudView = lazyHudView;
        }

        public void Setup()
        {
            var exploreGameRoot = _exploreGameRootPrefabFactory.GetInstance();

            _gameEngineUpdateBehaviourStitcher.Attach(exploreGameRoot.GameObject);
            _soundPlayingBehaviourStitcher.Attach(exploreGameRoot.GameObject);

            _guiBehaviorStitcher.Stitch(
                exploreGameRoot.GameObject,
                x => x.activeInHierarchy && x.name == "FollowCamera",
                _lazyHudView.Value,
                x => _noesisGuiHitTester.Setup((NoesisView)x)); 
            _unityGuiHitTester.Setup(
                _gameObjectManager
                    .FindAll(x => x.name == "Canvas")
                    .Single()
                    .GetComponent<GraphicRaycaster>(),
                _gameObjectManager
                    .FindAll(x => x.name == "EventSystem")
                    .Single()
                    .GetComponent<EventSystem>());

            var consoleObject = new GameObject()
            {
                name = "ConsoleCommands",
            };
            consoleObject.AddComponent<GlobalConsoleCommandsBehaviour>();
            consoleObject.AddComponent<ConsoleCommandsBehaviour>();
            consoleObject.transform.parent = exploreGameRoot.GameObject.transform;

            var mapPrefab = _mapPrefabFactory.CreateMap("Map");
            mapPrefab.GameObject.transform.parent = exploreGameRoot.GameObject.transform;

            _guiInputStitcher.Attach(exploreGameRoot.GameObject);
            _exploreSceneStartupInterceptorFacade.Intercept(exploreGameRoot.GameObject);

            _hasFollowCameraBehaviourStitcher.Attach(exploreGameRoot.GameObject);

            // FIXME: this is just for testing
            _mapManager.SwitchMap(new StringIdentifier("swamp"));
            var player = CreatePlayerInstance();
            player.GetOnly<IPositionBehavior>().SetPosition(40, -16);
            _mapGameObjectManager.MarkForAddition(player);
        }

        private IGameObject CreatePlayerInstance()
        {
            var context = _filterContextAmenity.CreateFilterContextForSingle(
                _filterContextAmenity.CreateRequiredAttribute(
                    _gameObjectIdentifiers.FilterContextTypeId,
                    _actorIdentifiers.ActorTypeIdentifier),
                _filterContextAmenity.CreateRequiredAttribute(
                    _actorIdentifiers.ActorDefinitionIdentifier,
                    new StringIdentifier("player")));
            var player = _actorGeneratorFacade
                .GenerateActors(
                    context,
                    new IGeneratorComponent[] { })
                .Single();
            return player;
        }
    }
}

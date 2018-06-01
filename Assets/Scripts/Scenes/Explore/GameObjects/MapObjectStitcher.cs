using Assets.Scripts.Api.GameObjects;
using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public sealed class MapObjectStitcher : IMapObjectStitcher
    {
        private readonly IPrefabStitcherFacade _prefabStitcherFacade;
        private readonly IIdentityBehaviourStitcher _identityBehaviourStitcher;
        private readonly IWorldLocationStitcher _worldLocationStitcher;
        private readonly IHasGameObjectBehaviourStitcher _hasGameObjectBehaviourStitcher;

        public MapObjectStitcher(
            IPrefabStitcherFacade prefabStitcherFacade,
            IIdentityBehaviourStitcher identityBehaviourStitcher,
            IWorldLocationStitcher worldLocationStitcher,
            IHasGameObjectBehaviourStitcher hasGameObjectBehaviourStitcher)
        {
            _prefabStitcherFacade = prefabStitcherFacade;
            _identityBehaviourStitcher = identityBehaviourStitcher;
            _worldLocationStitcher = worldLocationStitcher;
            _hasGameObjectBehaviourStitcher = hasGameObjectBehaviourStitcher;
        }

        public void Stitch(
            IGameObject gameObject,
            GameObject unityGameObject,
            string prefabResourceId)
        {
            // every map object needs a reference to the underlying game object
            _hasGameObjectBehaviourStitcher.Attach(
                gameObject,
                unityGameObject);

            // set an ID on the game object
            _identityBehaviourStitcher.Stitch(
                gameObject,
                unityGameObject);

            // stitch additional behaviours
            _prefabStitcherFacade.Stitch(
                unityGameObject,
                prefabResourceId);

            // move the game object to the right spot
            _worldLocationStitcher.Stitch(
                gameObject,
                unityGameObject);
        }
    }
}
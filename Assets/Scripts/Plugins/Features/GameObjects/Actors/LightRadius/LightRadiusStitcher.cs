using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Resources.Prefabs;

using Macerus.Plugins.Features.GameObjects.Actors.LightRadius;
using Macerus.Plugins.Features.Stats.Api;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public sealed class LightRadiusStitcher : ILightRadiusStitcher
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly ILightRadiusIdentifiers _lightRadiusIdentifiers;
        private readonly ITimeProvider _timeProvider;

        public LightRadiusStitcher(
            IPrefabCreator prefabCreator,
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            ILightRadiusIdentifiers lightRadiusIdentifiers,
            ITimeProvider timeProvider)
        {
            _prefabCreator = prefabCreator;
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _lightRadiusIdentifiers = lightRadiusIdentifiers;
            _timeProvider = timeProvider;
        }

        public ILightRadiusPrefab Stitch(IGameObject gameObject, GameObject unityGameObject)
        {
            var lightRadiusObject = _prefabCreator.Create<GameObject>("Mapping/Prefabs/LightRadius");
            lightRadiusObject.transform.position = new Vector3(0, 0, -1);
            lightRadiusObject.transform.SetParent(unityGameObject.transform, false);

            var lightRadiusPrefab = new LightRadiusPrefab(lightRadiusObject);
            var lightRadiusUpdateBehaviour = lightRadiusObject.AddComponent<LightRadiusUpdateBehaviour>();
            lightRadiusUpdateBehaviour.StatCalculationServiceAmenity = _statCalculationServiceAmenity;
            lightRadiusUpdateBehaviour.LightRadiusIdentifiers = _lightRadiusIdentifiers;
            lightRadiusUpdateBehaviour.TimeProvider = _timeProvider;
            lightRadiusUpdateBehaviour.LightRadiusPrefab = lightRadiusPrefab;
            lightRadiusUpdateBehaviour.GameObject = gameObject;

            return lightRadiusPrefab;
        }
    }
}
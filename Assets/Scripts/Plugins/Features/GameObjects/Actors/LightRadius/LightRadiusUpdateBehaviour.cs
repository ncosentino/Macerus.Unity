
using Assets.Scripts.Unity;

using Macerus.Plugins.Features.GameObjects.Actors.LightRadius;
using Macerus.Plugins.Features.Stats;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public sealed class LightRadiusUpdateBehaviour :
        MonoBehaviour,
        ILightRadiusUpdateBehaviour
    {
        private double _lastUpdate;

        public ITimeProvider TimeProvider { get; set; }

        public IGameObject GameObject { get; set; }

        public ILightRadiusPrefab LightRadiusPrefab { get; set; }

        public IStatCalculationServiceAmenity StatCalculationServiceAmenity { get; set; }

        public ILightRadiusIdentifiers LightRadiusIdentifiers { get; set; }

        private void Start()
        {
            UnityContracts.RequiresNotNull(this, TimeProvider, nameof(TimeProvider));
            UnityContracts.RequiresNotNull(this, GameObject, nameof(GameObject));
            UnityContracts.RequiresNotNull(this, LightRadiusPrefab, nameof(LightRadiusPrefab));
            UnityContracts.RequiresNotNull(this, StatCalculationServiceAmenity, nameof(StatCalculationServiceAmenity));
            UnityContracts.RequiresNotNull(this, LightRadiusIdentifiers, nameof(LightRadiusIdentifiers));
        }

        private void FixedUpdate()
        {
            var secondsSinceLastUpdate = TimeProvider.SecondsSinceStartOfGame - _lastUpdate;
            if (secondsSinceLastUpdate < 1)
            {
                return;
            }

            _lastUpdate = TimeProvider.SecondsSinceStartOfGame;

            var lightRadiusStats = StatCalculationServiceAmenity.GetStatValues(
                GameObject,
                new[]
                {
                    LightRadiusIdentifiers.RadiusStatIdentifier,
                    LightRadiusIdentifiers.IntensityStatIdentifier,
                    LightRadiusIdentifiers.RedStatIdentifier,
                    LightRadiusIdentifiers.GreenStatIdentifier,
                    LightRadiusIdentifiers.BlueStatIdentifier,
                });

            LightRadiusPrefab.Light.color = new Color(
                (float)lightRadiusStats[LightRadiusIdentifiers.RedStatIdentifier],
                (float)lightRadiusStats[LightRadiusIdentifiers.GreenStatIdentifier],
                (float)lightRadiusStats[LightRadiusIdentifiers.BlueStatIdentifier]);
            LightRadiusPrefab.Light.range = (float)lightRadiusStats[LightRadiusIdentifiers.RadiusStatIdentifier];
            LightRadiusPrefab.Light.intensity = (float)lightRadiusStats[LightRadiusIdentifiers.IntensityStatIdentifier];
        }
    }
}
using System.Linq;

using Assets.Scripts.Unity;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Stats;

using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.ResourceOrbs
{
    public sealed class ResourceOrbBehaviour :
        MonoBehaviour,
        IResourceOrbBehaviour
    {
        private double _lastUpdate;

        public ITimeProvider TimeProvider { get; set; }

        public IReadOnlyMapGameObjectManager MapGameObjectManager { get; set; }

        public IResourceOrbPrefab ResourceOrbPrefab { get; set; }

        public IStatCalculationServiceAmenity StatCalculationServiceAmenity { get; set; }

        public IIdentifier CurrentStatDefinitionId { get; set; }

        public IIdentifier MaximumStatDefinitionId { get; set; }

        private void Start()
        {
            UnityContracts.RequiresNotNull(this, TimeProvider, nameof(TimeProvider));
            UnityContracts.RequiresNotNull(this, MapGameObjectManager, nameof(MapGameObjectManager));
            UnityContracts.RequiresNotNull(this, ResourceOrbPrefab, nameof(ResourceOrbPrefab));
            UnityContracts.RequiresNotNull(this, StatCalculationServiceAmenity, nameof(StatCalculationServiceAmenity));
            UnityContracts.RequiresNotNull(this, CurrentStatDefinitionId, nameof(CurrentStatDefinitionId));
            UnityContracts.RequiresNotNull(this, MaximumStatDefinitionId, nameof(MaximumStatDefinitionId));
            UpdateFill(1);
        }

        private async void FixedUpdate()
        {
            var secondsSinceLastUpdate = TimeProvider.SecondsSinceStartOfGame - _lastUpdate;
            if (secondsSinceLastUpdate < 0.25)
            {
                return;
            }

            _lastUpdate = TimeProvider.SecondsSinceStartOfGame;

            var player = MapGameObjectManager
                .GameObjects
                .FirstOrDefault(x => x.Has<IPlayerControlledBehavior>());
            if (player == null)
            {
                UpdateFill(0);
                return;
            }

            var resources = await StatCalculationServiceAmenity.GetStatValuesAsync(
                player,
                new[] { CurrentStatDefinitionId, MaximumStatDefinitionId });
            var resourceCurrent = resources[CurrentStatDefinitionId];
            var resourceMaximum = resources[MaximumStatDefinitionId];
            UpdateFill(resourceMaximum == 0 ? 0 : resourceCurrent / resourceMaximum);
        }

        private void UpdateFill(double percentFull)
        {
            ResourceOrbPrefab.OrbFillImage.fillAmount = (float)percentFull;
        }
    }
}

using System.Linq;

using Assets.Scripts.Unity;

using Macerus.Api.Behaviors;

using NexusLabs.Contracts;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Enchantments.Stats;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.ResourceOrbs
{
    public sealed class ResourceOrbBehaviour : MonoBehaviour, IResourceOrbBehaviour
    {
        private double _lastUpdate;

        public ITimeProvider TimeProvider { get; set; }

        public IGameObjectManager GameObjectManager { get; set; }

        public IResourceOrbPrefab ResourceOrbPrefab { get; set; }

        public IStatCalculationService StatCalculationService { get; set; }

        public IIdentifier CurrentStatDefinitionId { get; set; }

        public IIdentifier MaximumStatDefinitionId { get; set; }

        private void Start()
        {
            UnityContracts.RequiresNotNull(this, TimeProvider, nameof(TimeProvider));
            UnityContracts.RequiresNotNull(this, GameObjectManager, nameof(GameObjectManager));
            UnityContracts.RequiresNotNull(this, ResourceOrbPrefab, nameof(ResourceOrbPrefab));
            UnityContracts.RequiresNotNull(this, StatCalculationService, nameof(StatCalculationService));
            UnityContracts.RequiresNotNull(this, CurrentStatDefinitionId, nameof(CurrentStatDefinitionId));
            UnityContracts.RequiresNotNull(this, MaximumStatDefinitionId, nameof(MaximumStatDefinitionId));
            UpdateFill(1);
        }

        private void FixedUpdate()
        {
            var secondsSinceLastUpdate = TimeProvider.SecondsSinceStartOfGame - _lastUpdate;
            if (secondsSinceLastUpdate < 0.25)
            {
                return;
            }

            _lastUpdate = TimeProvider.SecondsSinceStartOfGame;

            var player = GameObjectManager
                .GameObjects
                .FirstOrDefault(x => x.Has<IPlayerControlledBehavior>());
            if (player == null)
            {
                UpdateFill(0);
                return;
            }

            var context = new StatCalculationContext(
                new IComponent[0],
                new IEnchantment[0]);
            var resourceCurrent = StatCalculationService.GetStatValue(
                player,
                CurrentStatDefinitionId,
                context);
            var resourceMaximum = StatCalculationService.GetStatValue(
                player,
                MaximumStatDefinitionId,
                context);
            UpdateFill(resourceMaximum == 0 ? 0 : resourceCurrent / resourceMaximum);
        }

        private void UpdateFill(double percentFull)
        {
            ResourceOrbPrefab.OrbFillImage.fillAmount = (float)percentFull;
        }
    }
}

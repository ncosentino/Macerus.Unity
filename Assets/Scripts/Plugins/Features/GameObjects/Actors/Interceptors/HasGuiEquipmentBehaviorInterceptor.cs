using System.Linq;

using Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours;
using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public sealed class HasGuiEquipmentBehaviorInterceptor : IDiscoverableGameObjectBehaviorInterceptor
    {
        private readonly IHasGuiEquipmentBehaviourStitcher _hasGuiEquipmentBehaviourStitcher;

        public HasGuiEquipmentBehaviorInterceptor(IHasGuiEquipmentBehaviourStitcher hasGuiEquipmentBehaviourStitcher)
        {
            _hasGuiEquipmentBehaviourStitcher = hasGuiEquipmentBehaviourStitcher;
        }

        public void Intercept(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var hasEquipmentBehavior = gameObject
                .Get<IHasEquipmentBehavior>()
                .SingleOrDefault();
            if (hasEquipmentBehavior == null)
            {
                return;
            }

            // NOTE: can allow null because maybe it's a read-only view of equipment
            var canEquipBehavior = gameObject
                .Get<ICanEquipBehavior>()
                .SingleOrDefault();

            _hasGuiEquipmentBehaviourStitcher.Attach(
                unityGameObject,
                hasEquipmentBehavior,
                canEquipBehavior);
        }
    }
}
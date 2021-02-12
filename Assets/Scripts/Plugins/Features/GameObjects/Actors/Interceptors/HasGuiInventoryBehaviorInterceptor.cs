using System.Linq;

using Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours;
using Assets.Scripts.Scenes.Explore.GameObjects;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public sealed class HasGuiInventoryBehaviorInterceptor : IDiscoverableGameObjectBehaviorInterceptor
    {
        private readonly IHasGuiInventoryBehaviourStitcher _hasGuiInventoryBehaviourStitcher;

        public HasGuiInventoryBehaviorInterceptor(IHasGuiInventoryBehaviourStitcher hasGuiInventoryBehaviourStitcher)
        {
            _hasGuiInventoryBehaviourStitcher = hasGuiInventoryBehaviourStitcher;
        }

        public void Intercept(
            IGameObject gameObject, 
            GameObject unityGameObject)
        {
            // FIXME: this is obviously a hack... how do we control which container to find?
            var targetContainerId = new StringIdentifier("Inventory");

            var itemContainerBehavior = gameObject
                .Get<IItemContainerBehavior>()
                .SingleOrDefault(x => targetContainerId.Equals(x.ContainerId));
            if (itemContainerBehavior == null)
            {
                return;
            }

            _hasGuiInventoryBehaviourStitcher.Attach(
                unityGameObject,
                itemContainerBehavior);
        }
    }
}
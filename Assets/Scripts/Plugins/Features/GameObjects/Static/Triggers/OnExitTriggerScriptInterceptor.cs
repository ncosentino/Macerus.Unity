using System;
using System.Globalization;
using System.Linq;

using Assets.Scripts.Scenes.Explore.GameObjects;

using Macerus.Plugins.Features.GameObjects.Static.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static
{
    public sealed class OnExitTriggerScriptInterceptor : IDiscoverableGameObjectBehaviorInterceptor
    {
        private readonly IOnExitTriggerScriptBehaviourStitcher _onExitTriggerScriptBehaviourStitcher;

        public OnExitTriggerScriptInterceptor(IOnExitTriggerScriptBehaviourStitcher onExitTriggerScriptBehaviourStitcher)
        {
            _onExitTriggerScriptBehaviourStitcher = onExitTriggerScriptBehaviourStitcher;
        }

        public void Intercept(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var properties = gameObject
                .Get<IReadOnlyStaticGameObjectPropertiesBehavior>()
                .SingleOrDefault()
                ?.Properties;
            if (properties == null)
            {
                return;
            }

            if (!properties.TryGetValue(
                "OnExitTriggerScriptId",
                out var rawTriggerScriptId))
            {
                return;
            }

            var scriptId = new StringIdentifier(Convert.ToString(rawTriggerScriptId, CultureInfo.InvariantCulture));
            _onExitTriggerScriptBehaviourStitcher.Stitch(
                gameObject,
                unityGameObject,
                scriptId);
        }
    }
}

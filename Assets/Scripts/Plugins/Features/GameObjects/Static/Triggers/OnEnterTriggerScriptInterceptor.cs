using System;
using System.Globalization;
using System.Linq;

using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using Macerus.Plugins.Features.GameObjects.Static.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static.Triggers
{
    public sealed class OnEnterTriggerScriptInterceptor : IDiscoverableGameObjectBehaviorInterceptor
    {
        private readonly IOnEnterTriggerScriptBehaviourStitcher _onEnterTriggerScriptBehaviourStitcher;

        public OnEnterTriggerScriptInterceptor(IOnEnterTriggerScriptBehaviourStitcher onEnterTriggerScriptBehaviourStitcher)
        {
            _onEnterTriggerScriptBehaviourStitcher = onEnterTriggerScriptBehaviourStitcher;
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
                "OnEnterTriggerScriptId",
                out var rawTriggerScriptId))
            {
                return;
            }

            var scriptId = new StringIdentifier(Convert.ToString(rawTriggerScriptId, CultureInfo.InvariantCulture));
            _onEnterTriggerScriptBehaviourStitcher.Stitch(
                gameObject,
                unityGameObject,
                scriptId);
        }
    }
}

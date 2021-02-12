
using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static
{
    public sealed class OnEnterTriggerScriptBehaviour :
        MonoBehaviour,
        IOnEnterTriggerScriptBehaviour
    {
        public IIdentifier ScriptId { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                ScriptId,
                $"{nameof(ScriptId)} was not set on '{gameObject}.{this}'.");
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Debug.Log($"FIXME: trigger on-enter script ID '{ScriptId}'.");
        }
    }
}

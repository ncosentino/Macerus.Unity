
using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static
{
    public sealed class OnExitTriggerScriptBehaviour :
        MonoBehaviour,
        IOnExitTriggerScriptBehaviour
    {
        public IIdentifier ScriptId { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                ScriptId,
                $"{nameof(ScriptId)} was not set on '{gameObject}.{this}'.");
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log($"FIXME: trigger on-exit script ID '{ScriptId}'.");
        }
    }
}

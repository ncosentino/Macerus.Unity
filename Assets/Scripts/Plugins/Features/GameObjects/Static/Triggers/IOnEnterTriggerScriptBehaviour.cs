
using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static.Triggers
{
    public interface IOnEnterTriggerScriptBehaviour : IReadOnlyOnEnterTriggerScriptBehaviour
    {
        new IIdentifier ScriptId { get; set; }
    }
}

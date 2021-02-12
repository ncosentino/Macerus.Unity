
using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static
{
    public interface IOnExitTriggerScriptBehaviour : IReadOnlyOnExitTriggerScriptBehaviour
    {
        new IIdentifier ScriptId { get; set; }
    }
}

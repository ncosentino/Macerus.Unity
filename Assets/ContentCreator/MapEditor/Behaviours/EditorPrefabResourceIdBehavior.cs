
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class EditorPrefabResourceIdBehavior :
        BaseBehavior,
        IReadOnlyEditorPrefabResourceIdBehavior
    {
        public EditorPrefabResourceIdBehavior(IIdentifier prefabResourceId)
        {
            PrefabResourceId = prefabResourceId;
        }

        public IIdentifier PrefabResourceId { get; }
    }
}

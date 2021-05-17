
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public interface IReadOnlyEditorPrefabResourceIdBehavior : IBehavior
    {
        IIdentifier PrefabResourceId { get; }
    }
}

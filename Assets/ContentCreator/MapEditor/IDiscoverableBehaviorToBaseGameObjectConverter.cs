using ProjectXyz.Api.GameObjects.Behaviors;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public interface IDiscoverableBehaviorToBaseGameObjectConverter : IBehaviorToBaseGameObjectConverter
    {
        bool CanConvert(IBehavior behavior);
    }
}
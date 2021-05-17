using ProjectXyz.Shared.Game.Behaviors;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class EditorNameBehavior : BaseBehavior
    {
        public EditorNameBehavior(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
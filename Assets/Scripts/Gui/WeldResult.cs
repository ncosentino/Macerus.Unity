using ProjectXyz.Framework.ViewWelding.Api.Welders;

namespace Assets.Scripts.Gui
{
    public sealed class WeldResult : IWeldResult
    {
        public WeldResult(
            object parent,
            object child)
        {
            Parent = parent;
            Child = child;
        }

        public object Parent { get; set; }

        public object Child { get; set; }
    }
}

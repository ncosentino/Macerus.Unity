
using Noesis;

using ProjectXyz.Framework.ViewWelding.Api.Welders;

namespace Assets.Scripts.Gui.Noesis.ViewWelding
{
    public sealed class ContentControlWelder : ISimpleWelder
    {
        private readonly ContentControl _parent;
        private readonly object _child;

        public ContentControlWelder(
            ContentControl parent,
            object child)
        {
            _parent = parent;
            _child = child;
        }

        public void Weld()
        {
            _parent.Content = _child;
        }
    }
}

#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
using System.Windows.Controls;
#endif

using ProjectXyz.Framework.ViewWelding.Api.Welders;

namespace Assets.Scripts.Gui.Noesis.ViewWelding
{
    public sealed class ViewboxWelder : ISimpleWelder
    {
        private readonly Viewbox _parent;
        private readonly UIElement _child;

        public ViewboxWelder(
            Viewbox parent,
            UIElement child)
        {
            _parent = parent;
            _child = child;
        }

        public IWeldResult Weld()
        {
            _parent.Child = _child;
            return new WeldResult(_parent, _parent.Child);
        }
    }
}

using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;

namespace Assets.Scripts.Gui.Noesis.ViewWelding
{
    public sealed class NoesisGuiPrefabWelder : ISimpleWelder
    {
        private readonly IViewWelderFactory _viewWelderFactory;
        private readonly INoesisGuiPrefab _parent;
        private readonly object _child;

        public NoesisGuiPrefabWelder(
            IViewWelderFactory viewWelderFactory,
            INoesisGuiPrefab parent,
            object child)
        {
            _viewWelderFactory = viewWelderFactory;
            _parent = parent;
            _child = child;
        }

        public void Weld()
        {
            _viewWelderFactory
                .Create<ISimpleWelder>(_parent.ContainerControl, _child)
                .Weld();
        }
    }
}

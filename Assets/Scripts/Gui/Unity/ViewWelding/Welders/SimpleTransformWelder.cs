using ProjectXyz.Framework.ViewWelding.Api.Welders;
using UnityEngine;

namespace Assets.Scripts.Gui.Unity.ViewWelding.Welders
{
    public sealed class SimpleTransformWelder : ISimpleWelder
    {
        private readonly Transform _parent;
        private readonly Transform _child;

        public SimpleTransformWelder(
            Transform parent,
            Transform child)
        {
            _parent = parent;
            _child = child;
        }

        public IWeldResult Weld()
        {
            _child.SetParent(
                _parent,
                false);
            return new WeldResult(_parent, _child);
        }
    }
}

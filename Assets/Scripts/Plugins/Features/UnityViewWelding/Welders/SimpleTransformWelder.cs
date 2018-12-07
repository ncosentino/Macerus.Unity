using ProjectXyz.Framework.ViewWelding.Api.Welders;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.UnityViewWelding.Welders
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

        public void Weld()
        {
            _child.SetParent(
                _parent,
                false);
        }
    }
}

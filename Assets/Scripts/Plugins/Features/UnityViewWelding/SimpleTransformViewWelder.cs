using Assets.Scripts.Plugins.Features.UnityViewWelding.Api;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.UnityViewWelding
{
    public sealed class SimpleTransformViewWelder : ISimpleViewWelder
    {
        private readonly Transform _parent;
        private readonly Transform _child;

        public SimpleTransformViewWelder(Transform parent, Transform child)
        {
            _parent = parent;
            _child = child;
        }

        public void Weld() => Weld(new SimpleViewWeldingOptions());

        public void Weld(ISimpleViewWeldingOptions simpleViewWeldingOptions)
        {
            _child.SetParent(
                _parent,
                false);

            if (simpleViewWeldingOptions.OrderFirst)
            {
                _child.SetAsFirstSibling();
            }
            else if (simpleViewWeldingOptions.OrderLast)
            {
                _child.SetAsLastSibling();
            }
        }
    }
}

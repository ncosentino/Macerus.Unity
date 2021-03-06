using Assets.Scripts.Gui.UnityViewWelding.Api;

using UnityEngine;

namespace Assets.Scripts.Gui.UnityViewWelding.Welders
{
    public sealed class TransformStackViewWelder : IStackViewWelder
    {
        private readonly Transform _parent;
        private readonly Transform _child;

        public TransformStackViewWelder(Transform parent, Transform child)
        {
            _parent = parent;
            _child = child;
        }

        public void Weld() => Weld(new StackViewWeldingOptions());

        public void Weld(IStackViewWeldingOptions weldOptions)
        {
            _child.SetParent(
                _parent,
                false);

            if (weldOptions.OrderFirst)
            {
                _child.SetAsFirstSibling();
            }
            else if (weldOptions.OrderLast)
            {
                _child.SetAsLastSibling();
            }
        }
    }
}

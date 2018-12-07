using System;
using ProjectXyz.Framework.ViewWelding.Api.Welders;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.UnityViewWelding.Welders
{
    public sealed class MarginGameObjectWelder : IMarginWelder
    {
        private readonly GameObject _parent;
        private readonly GameObject _child;

        public MarginGameObjectWelder(
            GameObject parent,
            GameObject child)
        {
            _parent = parent;
            _child = child;
        }

        public void Weld(IMarginWeldOptions weldOptions)
        {
            _child.transform.SetParent(
                _parent.transform,
                false);

            // set margin. this MUST come after set parent for it to work as expected.
            var transform = _child.GetComponent<RectTransform>();
            if (weldOptions.LeftMargin != 0)
            {
                RectTransformContract(transform);
                transform.SetInsetAndSizeFromParentEdge(
                    RectTransform.Edge.Left,
                    weldOptions.LeftMargin,
                    transform.rect.width);
            }

            if (weldOptions.RightMargin != 0)
            {
                RectTransformContract(transform);
                transform.SetInsetAndSizeFromParentEdge(
                    RectTransform.Edge.Right,
                    weldOptions.RightMargin,
                    transform.rect.width);
            }

            if (weldOptions.TopMargin != 0)
            {
                RectTransformContract(transform);
                transform.SetInsetAndSizeFromParentEdge(
                    RectTransform.Edge.Top,
                    weldOptions.TopMargin,
                    transform.rect.height);
            }

            if (weldOptions.BottomMargin != 0)
            {
                RectTransformContract(transform);
                transform.SetInsetAndSizeFromParentEdge(
                    RectTransform.Edge.Bottom,
                    weldOptions.BottomMargin,
                    transform.rect.height);
            }
        }

        private void RectTransformContract(RectTransform rectTransform)
        {
            if (rectTransform == null)
            {
                throw new InvalidOperationException(
                    $"A '{typeof(RectTransform)}' is required to perform the requested welding operation.");
            }
        }
    }
}
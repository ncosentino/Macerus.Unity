using System;
using ProjectXyz.Framework.ViewWelding.Api.Welders;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.UnityViewWelding.Welders
{
    public sealed class InsetGameObjectWelder : IInsetWelder
    {
        private readonly GameObject _parent;
        private readonly GameObject _child;

        public InsetGameObjectWelder(
            GameObject parent,
            GameObject child)
        {
            _parent = parent;
            _child = child;
        }

        public void Weld(IInsetWeldOptions weldOptions)
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
                    transform.rect.width - weldOptions.LeftMargin);
            }

            if (weldOptions.RightMargin != 0)
            {
                RectTransformContract(transform);
                transform.SetInsetAndSizeFromParentEdge(
                    RectTransform.Edge.Right,
                    weldOptions.RightMargin,
                    transform.rect.width - weldOptions.RightMargin);
            }

            if (weldOptions.TopMargin != 0)
            {
                RectTransformContract(transform);
                transform.SetInsetAndSizeFromParentEdge(
                    RectTransform.Edge.Top,
                    weldOptions.TopMargin,
                    transform.rect.height - weldOptions.TopMargin);
            }

            if (weldOptions.BottomMargin != 0)
            {
                RectTransformContract(transform);
                transform.SetInsetAndSizeFromParentEdge(
                    RectTransform.Edge.Bottom,
                    weldOptions.BottomMargin,
                    transform.rect.height - weldOptions.BottomMargin);
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
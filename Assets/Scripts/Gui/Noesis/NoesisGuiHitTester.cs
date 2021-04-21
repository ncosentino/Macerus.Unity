using System.Collections.Generic;

using Assets.Assets.Scripts.Gui.Noesis;

using NexusLabs.Contracts;

using Noesis;

using UnityEngine;

namespace Assets.Scripts.Gui.Noesis
{
    public sealed class NoesisGuiHitTester : INoesisGuiHitTester
    {
        private NoesisView _noesisView;

        public void Setup(NoesisView noesisView)
        {
            _noesisView = noesisView;
        }

        public IReadOnlyCollection<object> HitTest(Vector3 position)
        {
            Contract.RequiresNotNull(
                _noesisView,
                $"'{nameof(_noesisView)}' is not set. Did you call '{nameof(Setup)}'?");
            Contract.RequiresNotNull(
                _noesisView.Content,
                $"'{nameof(_noesisView)}' does not have assigned content. Did " +
                $"your owning camera object get destroyed?");

            var point = new Point(
                position.x,
                Screen.height - position.y);
            var hit = VisualTreeHelper.HitTest(
                _noesisView.Content,
                point);
            if (hit.VisualHit != null &&
                !((hit.VisualHit as FrameworkElement).DataContext is ITransparentToGameInteraction))
            {
                return new[] { hit.VisualHit };
            }

            return new object[] { };
        }
    }
}
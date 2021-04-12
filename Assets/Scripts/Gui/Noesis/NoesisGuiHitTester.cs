using System.Collections.Generic;

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

            if (_noesisView.Content == null)
            {
                return new object[] { };
            }

            var point = new Point(
                position.x,
                Screen.height - position.y);
            var hit = VisualTreeHelper.HitTest(
                _noesisView.Content,
                point);
            if (hit.VisualHit != null)
            {
                return new[] { hit.VisualHit };
            }

            return new object[] { };
        }
    }
}
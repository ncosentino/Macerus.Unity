using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.Scripts.Gui
{
    public sealed class GuiHitTesterFacade : IGuiHitTesterFacade
    {
        private readonly IReadOnlyCollection<IGuiHitTester> _hitTesters;

        public GuiHitTesterFacade(IEnumerable<IDiscoverableGuiHitTester> hitTesters)
        {
            _hitTesters = hitTesters.ToArray();
        }

        public IReadOnlyCollection<object> HitTest(Vector3 position)
        {
            foreach (var hitTester in _hitTesters)
            {
                var results = hitTester.HitTest(position);
                if (results.Any())
                {
                    return results;
                }
            }

            return new object[] { };
        }
    }
}
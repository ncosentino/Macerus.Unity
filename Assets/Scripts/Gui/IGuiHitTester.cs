using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Gui
{
    public interface IGuiHitTester
    {
        IReadOnlyCollection<object> HitTest(Vector3 position);
    }
}
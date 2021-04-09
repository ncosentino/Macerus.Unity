using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Gui
{
    public interface IGuiHitTester
    {
        IReadOnlyCollection<GameObject> HitTest(Vector3 position);
    }
}
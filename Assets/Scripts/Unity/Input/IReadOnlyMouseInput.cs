using UnityEngine;

namespace Assets.Scripts.Unity.Input
{
    public interface IReadOnlyMouseInput
    {
        bool IsPresent { get; }
        Vector3 Position { get; }
        Vector2 ScrollDelta { get; }
        bool SimulateMouseWithTouches { get; }

        bool GetMouseButton(int button);
        bool GetMouseButtonDown(int button);
        bool GetMouseButtonUp(int button);
    }
}
using UnityEngine;

namespace Assets.Scripts.Unity.Input
{
    using UnityInput = UnityEngine.Input;

    public sealed class MouseInput : IMouseInput
    {
        public bool IsPresent => UnityInput.mousePresent;

        public Vector3 Position => UnityInput.mousePosition;

        public Vector2 ScrollDelta => UnityInput.mouseScrollDelta;

        public bool SimulateMouseWithTouches
        {
            get => UnityInput.simulateMouseWithTouches;
            set => UnityInput.simulateMouseWithTouches = value;
        }

        public bool GetMouseButton(int button) => UnityInput.GetMouseButton(button);

        public bool GetMouseButtonDown(int button) => UnityInput.GetMouseButtonDown(button);

        public bool GetMouseButtonUp(int button) => UnityInput.GetMouseButtonUp(button);
    }
}

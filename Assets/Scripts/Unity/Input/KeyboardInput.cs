using UnityEngine;

namespace Assets.Scripts.Unity.Input
{
    using UnityInput = UnityEngine.Input;

    public sealed class KeyboardInput : IKeyboardInput
    {
        public bool GetKey(KeyCode keyCode) => UnityInput.GetKey(keyCode);

        public bool GetKeyUp(KeyCode keyCode) => UnityInput.GetKeyUp(keyCode);

        public bool GetKeyDown(KeyCode keyCode) => UnityInput.GetKeyDown(keyCode);
    }
}

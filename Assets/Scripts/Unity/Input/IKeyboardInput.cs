using UnityEngine;

namespace Assets.Scripts.Unity.Input
{
    public interface IKeyboardInput
    {
        bool GetKey(KeyCode keyCode);

        bool GetKeyUp(KeyCode keyCode);

        bool GetKeyDown(KeyCode keyCode);
    }
}
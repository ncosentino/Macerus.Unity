using UnityEngine;

namespace Assets.Scripts.Gui
{
    public static class GuiExtensionMethods
    {
        public static bool ToggleEnabled(this GameObject uiElement)
        {
            uiElement.transform.localScale = uiElement.transform.localScale == Vector3.zero
                ? Vector3.one
                : Vector3.zero;
            return uiElement.transform.localScale == Vector3.one;
        }
    }
}

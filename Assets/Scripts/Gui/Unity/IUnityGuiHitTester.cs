using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Unity
{
    public interface IUnityGuiHitTester : IGuiHitTester
    {
        void Setup(
            GraphicRaycaster graphicRaycaster,
            EventSystem eventSystem);
    }
}
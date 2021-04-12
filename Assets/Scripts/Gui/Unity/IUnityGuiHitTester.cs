using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Unity
{
    public interface IUnityGuiHitTester : IDiscoverableGuiHitTester
    {
        void Setup(
            GraphicRaycaster graphicRaycaster,
            EventSystem eventSystem);
    }
}
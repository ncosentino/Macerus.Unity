using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Unity
{
    public sealed class UnityGuiHitTester : IUnityGuiHitTester
    {
        private GraphicRaycaster _graphicRaycaster;
        private EventSystem _eventSystem;

        public void Setup(
            GraphicRaycaster graphicRaycaster,
            EventSystem eventSystem)
        {
            _graphicRaycaster = graphicRaycaster;
            _eventSystem = eventSystem;
        }

        public IReadOnlyCollection<object> HitTest(Vector3 position)
        {
            var pointerEventData = new PointerEventData(_eventSystem);
            pointerEventData.position = position;
            var resultAppendList = new List<RaycastResult>();
            _graphicRaycaster.Raycast(pointerEventData, resultAppendList);
            return resultAppendList.Select(x => x.gameObject).ToArray();
        }
    }
}
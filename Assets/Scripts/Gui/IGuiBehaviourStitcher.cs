using System;

using UnityEngine;

namespace Assets.Scripts.Gui
{
    public interface IGuiBehaviourStitcher
    {
        void Stitch(
            GameObject gameObject,
            Predicate<GameObject> findGuiCameraCallback,
            object viewToWeld,
            Action<MonoBehaviour> guiWeldedCallback);
    }
}
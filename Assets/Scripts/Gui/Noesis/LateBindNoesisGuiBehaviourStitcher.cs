using System;

using Assets.Scripts.Unity.GameObjects;

using ProjectXyz.Framework.ViewWelding.Api;

using UnityEngine;

namespace Assets.Scripts.Gui.Noesis
{
    public sealed class LateBindNoesisGuiBehaviourStitcher : ILateBindNoesisGuiBehaviourStitcher
    {
        private readonly IUnityGameObjectManager _unityGameObjectManager;
        private readonly IViewWelderFactory _viewWelderFactory;

        public LateBindNoesisGuiBehaviourStitcher(
            IUnityGameObjectManager unityGameObjectManager,
            IViewWelderFactory viewWelderFactory)
        {
            _unityGameObjectManager = unityGameObjectManager;
            _viewWelderFactory = viewWelderFactory;
        }

        public void Stitch(
            GameObject gameObject,
            Predicate<GameObject> findCameraCallback,
            object viewToWeld,
            Action<MonoBehaviour> guiWeldedCallback)
        {
            var noesisguistitchbehaviour = gameObject.AddComponent<LateBindNoesisGuiBehaviour>();
            noesisguistitchbehaviour.ViewWelderFactory = _viewWelderFactory;
            noesisguistitchbehaviour.UnityGameObjectManager = _unityGameObjectManager;
            noesisguistitchbehaviour.FindCameraCallback = findCameraCallback;
            noesisguistitchbehaviour.GuiWeldedCallback = guiWeldedCallback;
            noesisguistitchbehaviour.ViewToWeld = viewToWeld;
        }
    }
}

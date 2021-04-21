using System;
using System.Linq;

using Assets.Scripts.Gui.Noesis.Views.Resources;
using Assets.Scripts.Unity.GameObjects;

using NexusLabs.Contracts;
using Noesis;
using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;

using UnityEngine;

namespace Assets.Scripts.Gui.Noesis
{
    public sealed class LateBindNoesisGuiBehaviour : MonoBehaviour
    {
        public Predicate<GameObject> FindCameraCallback { get; set; }

        public Action<MonoBehaviour> GuiWeldedCallback { get; set; }

        public IUnityGameObjectManager UnityGameObjectManager { get; set; }

        public IViewWelderFactory ViewWelderFactory { get; set; }

        public object ViewToWeld  { get; set; }

        private void Start()
        {
            this.RequiresNotNull(FindCameraCallback, nameof(FindCameraCallback));
            this.RequiresNotNull(UnityGameObjectManager, nameof(UnityGameObjectManager));
            this.RequiresNotNull(ViewWelderFactory, nameof(ViewWelderFactory));
            this.RequiresNotNull(ViewToWeld, nameof(ViewToWeld));
        }

        private void FixedUpdate()
        {
            var cameras = UnityGameObjectManager
                .FindAll(x => FindCameraCallback(x))
                .Distinct()
                .ToArray();
            if (cameras.Length < 1)
            {
                return;
            }

            if (cameras.Length > 1)
            {
                throw new InvalidOperationException(
                    $"Expecting to find a single matching camera but found {cameras.Length}.");
            }

            var camera = cameras.Single();
            var noesisView = camera.AddComponent<NoesisView>();
            noesisView.RenderFlags |= RenderFlags.PPAA;
            var container = ViewWelderFactory
                .Create<ISimpleWelder>(
                    noesisView,
                    typeof(Container))
                .Weld()
                .Child;
            ViewWelderFactory
                .Create<ISimpleWelder>(
                    container,
                    ViewToWeld)
                .Weld();
            GuiWeldedCallback?.Invoke(noesisView);

            Destroy(this);
        }
    }
}

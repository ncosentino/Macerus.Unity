using System;
using System.Linq;

using Assets.Scripts.Gui.Noesis.Views.Resources;
using Assets.Scripts.Unity.GameObjects;

using NexusLabs.Contracts;

using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore
{
    public sealed class LateBindNoesisGuiBehaviour : MonoBehaviour
    {
        public Predicate<GameObject> FindCameraCallback { get; set; }

        public IUnityGameObjectManager UnityGameObjectManager { get; set; }

        public IViewWelderFactory ViewWelderFactory { get; set; }

        private void Start()
        {
            UnityContracts.RequiresNotNull(this, FindCameraCallback, nameof(FindCameraCallback));
            UnityContracts.RequiresNotNull(this, UnityGameObjectManager, nameof(UnityGameObjectManager));
            UnityContracts.RequiresNotNull(this, ViewWelderFactory, nameof(ViewWelderFactory));
        }

        private void Update()
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
            ViewWelderFactory
                .Create<ISimpleWelder>(
                    noesisView,
                    typeof(Container))
                .Weld();
            Destroy(this);
        }
    }
}

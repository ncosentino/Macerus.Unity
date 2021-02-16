using System;
using System.Linq;
using Assets.Scripts.Unity.GameObjects;
using NexusLabs.Contracts;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Camera
{
    public sealed class CameraAutoTargetBehaviour :
        MonoBehaviour,
        ICameraAutoTargetBehaviour
    {
        private float _triggerTime;

        public IUnityGameObjectManager GameObjectManager { get; set; }

        public ICameraTargetting CameraTargetting { get; set; }

        public TimeSpan SearchDelay { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                GameObjectManager,
                $"{nameof(GameObjectManager)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                CameraTargetting,
                $"{nameof(CameraTargetting)} was not set on '{gameObject}.{this}'.");

            ResetTriggerTime();
        }

        private void FixedUpdate()
        {
            if (CameraTargetting.CameraTarget != null ||
                Time.fixedTime < _triggerTime)
            {
                return;
            }

            ResetTriggerTime();

            var cameraTargetCandidate = GameObjectManager
                .FindAll(x => x.HasRequiredComponent<ICameraTarget>())
                .FirstOrDefault();
            CameraTargetting.SetTarget(cameraTargetCandidate?.transform);
        }

        private void ResetTriggerTime()
        {
            _triggerTime = Time.fixedTime + (float)SearchDelay.TotalSeconds;
        }
    }
}
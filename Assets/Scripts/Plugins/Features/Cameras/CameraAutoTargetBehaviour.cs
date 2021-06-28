using System;
using System.Linq;

using Assets.Scripts;
using Assets.Scripts.Unity.GameObjects;

using Macerus.Plugins.Features.Camera;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Cameras
{
    public sealed class CameraAutoTargetBehaviour : MonoBehaviour
    {
        private float _triggerTime;

        public IUnityGameObjectManager GameObjectManager { get; set; }

        public ICameraTargetting CameraTargetting { get; set; }

        public IObservableCameraManager CameraManager { get; set; }

        public TimeSpan SearchDelay { get; set; }

        private void Start()
        {
            this.RequiresNotNull(GameObjectManager, nameof(GameObjectManager));
            this.RequiresNotNull(CameraTargetting, nameof(CameraTargetting));
            this.RequiresNotNull(CameraManager, nameof(CameraManager));

            CameraManager.FollowTargetChanged += CameraManager_FollowTargetChanged;

            ResetTriggerTime();
        }

        private void OnDestroy()
        {
            CameraManager.FollowTargetChanged -= CameraManager_FollowTargetChanged;
        }

        private void FixedUpdate()
        {
            if (CameraTargetting.CameraTarget != null ||
                Time.fixedTime < _triggerTime)
            {
                return;
            }

            ResetTriggerTime();

            var targetGameObject = CameraManager.FollowTarget;
            var targetUnityGameObject = targetGameObject == null
                ? null
                : GameObjectManager
                    .FindAll(x => x.GetGameObject() == targetGameObject)
                    .FirstOrDefault();
            CameraTargetting.SetTarget(targetUnityGameObject?.transform);
        }

        private void ResetTriggerTime()
        {
            _triggerTime = Time.fixedTime + (float)SearchDelay.TotalSeconds;
        }

        private void CameraManager_FollowTargetChanged(object sender, EventArgs e)
        {
            CameraTargetting.SetTarget(null);
            _triggerTime = 0;
        }
    }
}
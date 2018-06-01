using System;
using System.Linq;
using Assets.Scripts.Unity.GameObjects;
using ProjectXyz.Framework.Contracts;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Camera
{
    public sealed class CameraAutoTargetBehaviour :
        MonoBehaviour,
        ICameraAutoTargetBehaviour
    {
        private float _triggerTime;

        public IGameObjectManager GameObjectManager { get; set; }

        public ICameraTargetting CameraTargetting { get; set; }

        public TimeSpan SearchDelay { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                GameObjectManager,
                $"{nameof(GameObjectManager)} was not set.");
            Contract.RequiresNotNull(
                CameraTargetting,
                $"{nameof(CameraTargetting)} was not set.");

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
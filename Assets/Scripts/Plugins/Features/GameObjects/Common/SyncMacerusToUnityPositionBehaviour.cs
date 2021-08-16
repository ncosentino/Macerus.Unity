using System;

using Assets.Scripts.Unity.Threading;

using NexusLabs.Contracts;

using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    /// <summary>
    /// Responsible for synchronizing backend controlled world location
    /// to the front-end Unity.
    /// </summary>
    public sealed class SyncMacerusToUnityPositionBehaviour : MonoBehaviour
    {
        public IObservablePositionBehavior ObservablePositionBehavior { get; set; }

        public IObservableSizeBehavior ObservableSizeBehavior { get; set; }

        public IMacerusToUnityPositionSynchronizer MacerusToUnityPositionSynchronizer { get; set; }

        public IDispatcher Dispatcher { get; set; }

        private void Start()
        {
            this.RequiresNotNull(MacerusToUnityPositionSynchronizer, nameof(MacerusToUnityPositionSynchronizer));
            this.RequiresNotNull(ObservablePositionBehavior, nameof(ObservablePositionBehavior));
            this.RequiresNotNull(ObservableSizeBehavior, nameof(ObservableSizeBehavior));
            this.RequiresNotNull(Dispatcher, nameof(Dispatcher));

            ObservablePositionBehavior.PositionChanged += PositionBehavior_PositionChanged;
            ObservableSizeBehavior.SizeChanged += ObservableSizeBehavior_SizeChanged;

            // sync macerus (source of truth) to unity
            MacerusToUnityPositionSynchronizer.SynchronizeMacerusToUnityPosition(
                gameObject,
                ObservablePositionBehavior.X,
                ObservablePositionBehavior.Y);
            MacerusToUnityPositionSynchronizer.SynchronizeMacerusToUnitySize(
                gameObject,
                ObservableSizeBehavior.Width,
                ObservableSizeBehavior.Height);
        }

        private void OnDestroy()
        {
            if (ObservablePositionBehavior != null)
            {
                ObservablePositionBehavior.PositionChanged -= PositionBehavior_PositionChanged;
            }

            if (ObservableSizeBehavior != null)
            {
                ObservableSizeBehavior.SizeChanged -= ObservableSizeBehavior_SizeChanged;
            }
        }

        private void ObservableSizeBehavior_SizeChanged(
            object sender,
            EventArgs e)
        {
            Dispatcher.RunOnMainThread(() =>
            {
                MacerusToUnityPositionSynchronizer.SynchronizeMacerusToUnitySize(
                gameObject,
                ObservableSizeBehavior.Width,
                ObservableSizeBehavior.Height);
            });
        }

        private void PositionBehavior_PositionChanged(
            object sender,
            EventArgs e)
        {
            Dispatcher.RunOnMainThread(() =>
            {
                MacerusToUnityPositionSynchronizer.SynchronizeMacerusToUnityPosition(
                gameObject,
                ObservablePositionBehavior.X,
                ObservablePositionBehavior.Y);
            });
        }
    }
}
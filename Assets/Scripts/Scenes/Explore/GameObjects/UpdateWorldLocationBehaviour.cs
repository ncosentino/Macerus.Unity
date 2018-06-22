using System;
using Assets.Scripts.Unity.Threading;
using Macerus.Api.Behaviors;
using ProjectXyz.Framework.Contracts;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public sealed class UpdateWorldLocationBehaviour :
        MonoBehaviour,
        IUpdateWorldLocatiobBehaviour
    {
        public IObservableWorldLocationBehavior ObservableWorldLocationBehavior { get; set; }

        public IDispatcher Dispatcher { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                ObservableWorldLocationBehavior,
                $"{nameof(ObservableWorldLocationBehavior)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                Dispatcher,
                $"{nameof(Dispatcher)} was not set on '{gameObject}.{this}'.");
            ObservableWorldLocationBehavior.WorldLocationChanged += ObservableWorldLocationBehavior_WorldLocationChanged;
            SyncLocation();
        }

        private void OnDestroy()
        {
            if (ObservableWorldLocationBehavior != null)
            {
                ObservableWorldLocationBehavior.WorldLocationChanged -= ObservableWorldLocationBehavior_WorldLocationChanged;
            }
        }

        private void SyncLocation()
        {
            gameObject.transform.position = new Vector3(
                (float)ObservableWorldLocationBehavior.X,
                (float)ObservableWorldLocationBehavior.Y,
                -1);
        }

        private void ObservableWorldLocationBehavior_WorldLocationChanged(
            object sender,
            EventArgs e) => Dispatcher.RunOnMainThread(SyncLocation);
    }
}
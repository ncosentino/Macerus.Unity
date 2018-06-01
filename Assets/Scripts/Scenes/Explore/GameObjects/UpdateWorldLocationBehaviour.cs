using System;
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

        private void Start()
        {
            Contract.RequiresNotNull(
                ObservableWorldLocationBehavior,
                $"{nameof(ObservableWorldLocationBehavior)} was not set on '{gameObject}.{this}'.");
            ObservableWorldLocationBehavior.WorldLocationChanged += ObservableWorldLocationBehavior_WorldLocationChanged;
            SyncLocation();
        }

        private void SyncLocation()
        {
            Debug.Log($"Syncing location for '{gameObject}'...");
            gameObject.transform.position = new Vector3(
                (float)ObservableWorldLocationBehavior.X,
                (float)ObservableWorldLocationBehavior.Y,
                -1);
        }

        private void ObservableWorldLocationBehavior_WorldLocationChanged(
            object sender,
            EventArgs e) => SyncLocation();
    }
}
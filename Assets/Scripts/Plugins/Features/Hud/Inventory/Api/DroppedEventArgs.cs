
using System;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public sealed class DroppedEventArgs : EventArgs
    {
        public DroppedEventArgs(GameObject dropped, GameObject droppedOnto)
        {
            Dropped = dropped;
            DroppedOnto = droppedOnto;
        }

        public GameObject Dropped { get; }

        public GameObject DroppedOnto { get; }
    }
}
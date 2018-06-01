using Assets.Scripts.Api.GameObjects;
using Assets.Scripts.Plugins.Features.Actors.UnityBehaviours;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors
{
    public sealed class PlayerPrefabStitcher
    {
        public void Stitch(
            GameObject gameObject,
            string prefabResourceId)
        {
            gameObject.AddComponent<PlayerInputControlsBehaviour>();
        }
    }
}

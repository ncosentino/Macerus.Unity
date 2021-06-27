using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IPlayerQuickSlotControlsBehaviourStitcher
    {
        void Attach(GameObject unityGameObject, IGameObject actor);
    }
}
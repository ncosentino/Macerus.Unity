using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public sealed class HasGameObjectBehaviour :
        MonoBehaviour,
        IHasGameObject
    {
        public IGameObject GameObject { get; set; }
    }
}
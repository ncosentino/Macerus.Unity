using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    public sealed class HasGameObjectBehaviour :
        MonoBehaviour,
        IHasGameObject
    {
        public IGameObject GameObject { get; set; }
    }
}
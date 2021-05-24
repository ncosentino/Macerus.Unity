using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class ItemContainerBehaviour :
        MonoBehaviour,
        IConvertableBehaviour
    {
        public string ContainerId;

        public IReadOnlyCollection<IGameObject> ItemsReference { get; set; }
    }
}

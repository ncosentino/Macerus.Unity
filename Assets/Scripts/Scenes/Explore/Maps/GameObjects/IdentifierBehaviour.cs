using ProjectXyz.Api.Framework;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Maps.GameObjects
{
    public sealed class IdentifierBehaviour :
        MonoBehaviour,
        IIdentifierBehaviour
    {
        public IIdentifier Id { get; set; }
    }
}

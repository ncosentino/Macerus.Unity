using ProjectXyz.Api.Framework;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public sealed class IdentifierBehaviour :
        MonoBehaviour,
        IIdentifierBehaviour
    {
        public IIdentifier Id { get; set; }
    }
}

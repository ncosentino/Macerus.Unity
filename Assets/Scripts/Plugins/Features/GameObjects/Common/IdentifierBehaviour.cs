using Assets.Scripts.Api.GameObjects;
using ProjectXyz.Api.Framework;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    public sealed class IdentifierBehaviour :
        MonoBehaviour,
        IIdentifierBehaviour
    {
        public IIdentifier Id { get; set; }
    }
}

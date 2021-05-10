using System;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class IdentifierBehaviour :
        MonoBehaviour,
        IConvertableBehaviour
    {
        public string Id = Guid.NewGuid().ToString();
    }
}

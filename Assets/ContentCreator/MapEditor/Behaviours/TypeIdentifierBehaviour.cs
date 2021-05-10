using System;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class TypeIdentifierBehaviour :
        MonoBehaviour,
        IConvertableBehaviour
    {
        public string TypeId;
    }
}

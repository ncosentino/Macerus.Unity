
using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class ContainerInteractableBehaviour :
        MonoBehaviour,
        IConvertableBehaviour
    {
        public bool DestroyOnUse;

        public bool AutomaticInteraction;

        public bool TransferItemsOnActivate;
    }
}

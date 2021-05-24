
using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class ContainerGenerateItemsBehaviour :
        MonoBehaviour,
        IConvertableBehaviour
    {
        public string DropTableId;

        public bool HasGeneratedItems;
    }
}

using Assets.Scripts.Unity.Resources;
using ProjectXyz.Framework.Contracts;
using UnityEngine;

namespace Assets.Scripts.Gui.Hud.Inventory
{
    public sealed class ItemListBehaviour :
        MonoBehaviour,
        IItemListBehaviour
    {
        public IPrefabCreator PrefabCreator { get; set; }

        public string ItemListEntryPrefabResource { get; set; }

        public GameObject ListControlContent { get; set; }

        private void Start ()
        {
            Contract.RequiresNotNull(
                PrefabCreator,
                $"{nameof(PrefabCreator)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNullOrEmpty(
                ItemListEntryPrefabResource,
                $"{nameof(ItemListEntryPrefabResource)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                ListControlContent,
                $"{nameof(ListControlContent)} was not set on '{gameObject}.{this}'.");

            // FIXME: just to test
            var itemEntry = PrefabCreator.Create<GameObject>(ItemListEntryPrefabResource);
            itemEntry.transform.SetParent(ListControlContent.transform, false);
        }
	
        private void Update ()
        {
        }
    }
}

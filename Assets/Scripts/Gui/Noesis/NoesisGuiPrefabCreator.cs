using Assets.Scripts.Unity.Resources.Prefabs;

using UnityEngine;

namespace Assets.Scripts.Gui.Noesis
{
    public sealed class NoesisGuiPrefabCreator : IGuiPrefabCreator
    {
        private readonly IPrefabCreator _prefabCreator;

        public NoesisGuiPrefabCreator(IPrefabCreator prefabCreator)
        {
            _prefabCreator = prefabCreator;
        }

        public IGuiPrefab Create()
        {
            var prefab = _prefabCreator.Create<GameObject>("Gui/Prefabs/GUI");
            prefab.transform.parent = null;
            prefab.name = "Noesis GUI";

            var guiPrefab = new NoesisGuiPrefab(prefab);
            return guiPrefab;
        }
    }
}

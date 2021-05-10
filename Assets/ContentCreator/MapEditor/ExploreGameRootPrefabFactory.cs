using System.Linq;

using Assets.Scripts.Unity.GameObjects;

namespace Assets.ContentCreator.MapEditor
{
    public sealed class ExploreGameRootPrefabFactory : IExploreGameRootPrefabFactory
    {
        private readonly IUnityGameObjectManager _unityGameObjectManager;

        public ExploreGameRootPrefabFactory(IUnityGameObjectManager unityGameObjectManager)
        {
            _unityGameObjectManager = unityGameObjectManager;
        }

        public IExploreGameRootPrefab GetInstance()
        {
            var rootGameObject = _unityGameObjectManager
                .FindAll(x => x.name == "Game")
                .Single();
            var prefab = new ExploreGameRootPrefab(rootGameObject);
            return prefab;
        }
    }
}

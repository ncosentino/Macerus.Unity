namespace Assets.Scripts.Unity.Resources
{
    using Object = UnityEngine.Object;

    public sealed class PrefabCreator : IPrefabCreator
    {
        private readonly IResourceLoader _resourceLoader;

        public PrefabCreator(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
        }

        public TGameObject Create<TGameObject>(string relativePrefabPathWithinResources)
            where TGameObject : Object
        {
            var resource = _resourceLoader.Load<TGameObject>(relativePrefabPathWithinResources);
            var instantiated = Object.Instantiate(resource);
            return instantiated;
        }
    }
}

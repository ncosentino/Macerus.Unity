using System;

namespace Assets.Scripts.Unity.Resources.Prefabs
{
    public interface IPrefabCreatorRegistrar
    {
        void Register<TPrefab>(PrefabFactoryDelegate factory)
            where TPrefab : IPrefab;

        void Register(
            Type type,
            PrefabFactoryDelegate factory);
    }
}
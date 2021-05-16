using System;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    // FIXME: re-enable this
    //public sealed class SpawnTemplatePropertiesBehaviorConverter : IDiscoverableGameObjectToBehaviorConverter
    //{
    //    private readonly IGameObjectConverter _gameObjectConverter;
    //    private readonly IGameObjectFactory _gameObjectFactory;

    //    public SpawnTemplatePropertiesBehaviorConverter(
    //        IGameObjectConverter gameObjectConverter,
    //        IGameObjectFactory gameObjectFactory)
    //    {
    //        _gameObjectConverter = gameObjectConverter;
    //        _gameObjectFactory = gameObjectFactory;
    //    }

    //    public bool CanConvert(IBehavior behavior) =>
    //        behavior is IReadOnlySpawnTemplatePropertiesBehavior;

    //    public bool CanConvert(GameObject unityGameObject) =>
    //        "SpawnTemplatePropertiesBehavior".Equals(
    //            unityGameObject.name,
    //            StringComparison.OrdinalIgnoreCase);

    //    public GameObject Convert(IBehavior behavior)
    //    {
    //        var castedBehavior = (IReadOnlySpawnTemplatePropertiesBehavior)behavior;
    //        var unityGameObject = _gameObjectConverter.Convert(castedBehavior
    //            .TemplateToSpawn
    //            .Behaviors);
    //        return unityGameObject;
    //    }

    //    public IBehavior Convert(GameObject unityGameObject)
    //    {
    //        var behaviors = _gameObjectConverter.Convert(unityGameObject);
    //        var spawnTemplateObject = _gameObjectFactory.Create(behaviors);
    //        var spawnTemplatePropertiesBehavior = new SpawnTemplatePropertiesBehavior(spawnTemplateObject);
    //        return spawnTemplatePropertiesBehavior;
    //    }
    //}
}
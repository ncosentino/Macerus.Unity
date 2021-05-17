using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor
{
    public interface ISceneToMapConverter
    {
        IEnumerable<IGameObject> ConvertTiles(GameObject mapGameObject);

        IEnumerable<IGameObject> ConvertGameObjects(GameObject mapGameObject);
    }
}

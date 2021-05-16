
using UnityEngine;

namespace Assets.ContentCreator.MapEditor
{
    public interface ISceneToMapConverter
    {
        void Convert(GameObject mapGameObject);
    }
}

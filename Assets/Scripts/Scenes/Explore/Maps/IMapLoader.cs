using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Maps
{
    public interface IMapLoader
    {
        void LoadMap(GameObject mapGameObject, string mapResourcePath);
    }
}
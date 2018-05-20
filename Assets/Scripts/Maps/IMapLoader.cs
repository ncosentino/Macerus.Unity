using UnityEngine;

namespace Assets.Scripts.Maps
{
    public interface IMapLoader
    {
        void LoadMap(GameObject mapGameObject, string mapResourcePath);
    }
}
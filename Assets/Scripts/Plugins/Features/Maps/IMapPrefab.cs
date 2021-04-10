using Assets.Scripts.Unity.Resources.Prefabs;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Plugins.Features.Maps
{
    public interface IMapPrefab : IPrefab
    {
        GameObject GameObjectLayer { get; }

        Tilemap Tilemap { get; }
    }
}
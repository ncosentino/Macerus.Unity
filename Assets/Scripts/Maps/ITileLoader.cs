using Tiled.Net.Layers;
using Tiled.Net.Tilesets;
using UnityEngine;

namespace Assets.Scripts.Maps
{
    public interface ITileLoader
    {
        GameObject CreateTile(
            ITileset tileset,
            IMapLayerTile tile,
            int x,
            int y,
            int z);
    }
}
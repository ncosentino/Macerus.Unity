using System.IO;
using System.Linq;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Resources.Sprites;
using Tiled.Net.Layers;
using Tiled.Net.Tilesets;
using UnityEngine;

namespace Assets.Scripts.Maps
{
    public sealed class TileLoader : ITileLoader
    {
        private readonly ITilesetSpriteResourceResolver _tilesetSpriteResourceResolver;
        private readonly ISpriteLoader _spriteLoader;

        public TileLoader(
            ITilesetSpriteResourceResolver tilesetSpriteResourceResolver,
            ISpriteLoader spriteLoader)
        {
            _tilesetSpriteResourceResolver = tilesetSpriteResourceResolver;
            _spriteLoader = spriteLoader;
        }

        public GameObject CreateTile(
            ITileset tileset,
            IMapLayerTile tile,
            int x,
            int y,
            int z)
        {
            var relativeResourcePath = _tilesetSpriteResourceResolver.ResolveResourcePath(tileset);
            var spriteResourceName = $"{Path.GetFileNameWithoutExtension(relativeResourcePath)}_{tile.Gid - tileset.FirstGid}";

            var tileObject = Object.Instantiate(Resources.Load(
                "Mapping/Prefabs/Tile",
                typeof(GameObject))) as GameObject;
            tileObject.name = $"Tile ({x}x{y})";
            tileObject.transform.position = new Vector3(
                x,
                y,
                z);

            var renderer = tileObject.GetComponent<SpriteRenderer>();
            renderer.sprite = _spriteLoader.SpriteFromMultiSprite(
                relativeResourcePath,
                spriteResourceName);

            return tileObject;
        }
    }
}
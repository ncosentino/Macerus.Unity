using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.Resources;
using Assets.Scripts.Unity.Resources.Sprites;

using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps
{
    public sealed class TileLoader : ITileLoader
    {
        private readonly ISpriteLoader _spriteLoader;
        private readonly IPrefabCreator _prefabCreator;

        public TileLoader(
            ISpriteLoader spriteLoader,
            IPrefabCreator prefabCreator)
        {;
            _spriteLoader = spriteLoader;
            _prefabCreator = prefabCreator;
        }

        public GameObject CreateTile(
            int x,
            int y,
            int z,
            string relativeResourcePath,
            string spriteResourceName)
        {
            var tileObject = _prefabCreator.Create<GameObject>("Mapping/Prefabs/Tile");
            tileObject.name = $"Tile ({x}x{y})";
            tileObject.transform.position = new Vector3(
                x,
                y,
                z);

            var renderer = tileObject.GetComponent<SpriteRenderer>();

            // FIXME: resource path/name to ID conversion
            var sprite = _spriteLoader.SpriteFromMultiSprite(
                new StringIdentifier(relativeResourcePath),
                new StringIdentifier(spriteResourceName));
            renderer.sprite = sprite;

            return tileObject;
        }
    }
}
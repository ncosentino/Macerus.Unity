using Assets.Scripts.Unity.Resources;
using Assets.Scripts.Unity.Resources.Sprites;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Maps
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
            renderer.sprite = _spriteLoader.SpriteFromMultiSprite(
                relativeResourcePath,
                spriteResourceName);

            return tileObject;
        }
    }
}
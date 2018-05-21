using Assets.Scripts.Unity.Resources.Sprites;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Maps
{
    public sealed class TileLoader : ITileLoader
    {
        private readonly ISpriteLoader _spriteLoader;

        public TileLoader(ISpriteLoader spriteLoader)
        {;
            _spriteLoader = spriteLoader;
        }

        public GameObject CreateTile(
            int x,
            int y,
            int z,
            string relativeResourcePath,
            string spriteResourceName)
        {
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
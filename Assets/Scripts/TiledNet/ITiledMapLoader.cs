using ProjectXyz.Api.Framework;
using Tiled.Net.Maps;

namespace Assets.Scripts.TiledNet
{
    public interface ITiledMapLoader
    {
        ITiledMap LoadMap(IIdentifier mapId);
    }
}
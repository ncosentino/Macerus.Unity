using ProjectXyz.Api.Framework;
using Tiled.Net.Maps;

namespace Assets.Scripts.Plugins.Features.Maps.TiledNet
{
    public interface ITiledMapLoader
    {
        ITiledMap LoadMap(IIdentifier mapId);
    }
}
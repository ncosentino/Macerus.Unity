using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Mapping.Api;
using Tiled.Net.Maps;

namespace Assets.Scripts.Plugins.Features.Maps.TiledNet
{
    public interface ITiledNetToMapConverter
    {
        IMap Convert(
            IIdentifier mapId,
            ITiledMap tiledMap);
    }
}
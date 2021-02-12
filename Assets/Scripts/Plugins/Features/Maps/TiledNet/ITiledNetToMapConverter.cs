using ProjectXyz.Api.Framework;
using ProjectXyz.Game.Interface.Mapping;
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
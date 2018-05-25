using ProjectXyz.Api.Framework;
using ProjectXyz.Game.Interface.Mapping;
using Tiled.Net.Maps;

namespace Assets.Scripts.Scenes.Explore.Maps.TiledNet
{
    public interface ITiledNetToMapConverter
    {
        IMap Convert(
            IIdentifier mapId,
            ITiledMap tiledMap);
    }
}
using ProjectXyz.Game.Interface.Mapping;
using Tiled.Net.Maps;

namespace Assets.Scripts.Scenes.Explore.Maps.TiledNet
{
    public interface ITiledNetToMapConverter
    {
        IMap Convert(ITiledMap tiledMap);
    }
}
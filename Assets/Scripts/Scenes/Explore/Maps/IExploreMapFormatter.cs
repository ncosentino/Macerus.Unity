using ProjectXyz.Game.Interface.Mapping;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Maps
{
    public interface IExploreMapFormatter
    {
        void FormatMap(
            GameObject mapObject,
            IMap map);
    }
}
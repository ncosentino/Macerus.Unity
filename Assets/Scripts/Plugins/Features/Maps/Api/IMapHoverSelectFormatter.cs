
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface IMapHoverSelectFormatter
    {
        void HoverSelectTile(Vector2Int? position);
    }
}
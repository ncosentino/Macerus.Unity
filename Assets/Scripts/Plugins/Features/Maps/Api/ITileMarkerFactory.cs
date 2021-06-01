using System;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface ITileMarkerFactory
    {
        GameObject CreateTileMarker(string name, Vector2 position, Color color, TimeSpan? duration);
    }
}
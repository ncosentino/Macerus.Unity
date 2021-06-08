using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface IMapTraversableHighlighter
    {
        void SetTraversableTiles(IEnumerable<Vector2Int> traversableTiles);
    }
}
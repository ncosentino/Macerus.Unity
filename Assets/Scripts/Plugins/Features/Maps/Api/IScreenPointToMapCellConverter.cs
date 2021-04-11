using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface IScreenPointToMapCellConverter
    {
        Vector3Int Convert(Vector3 screenPoint);
    }
}
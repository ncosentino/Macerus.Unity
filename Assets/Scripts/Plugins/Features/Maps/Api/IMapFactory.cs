using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface IMapFactory
    {
        GameObject CreateMap();
    }
}
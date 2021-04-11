namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface IMapPrefabFactory
    {
        IMapPrefab CreateMap(string mapObjectName);
    }
}
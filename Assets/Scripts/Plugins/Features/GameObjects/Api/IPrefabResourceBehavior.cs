namespace Assets.Scripts.Plugins.Features.GameObjects.Api
{
    public interface IPrefabResourceBehavior : IReadOnlyPrefabResourceBehavior
    {
        new string PrefabResourceId { get; set; }
    }
}
namespace Assets.Scripts.Api.GameObjects
{
    public interface IPrefabResourceBehavior : IReadOnlyPrefabResourceBehavior
    {
        new string PrefabResourceId { get; set; }
    }
}
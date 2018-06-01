using Assets.Scripts.Api.GameObjects;
using ProjectXyz.Shared.Game.Behaviors;

namespace Assets.Scripts.Shared.GameObjects
{
    public class HasPrefabResourceBehavior :
        BaseBehavior,
        IPrefabResourceBehavior
    {
        public string PrefabResourceId { get; set; }
    }
}

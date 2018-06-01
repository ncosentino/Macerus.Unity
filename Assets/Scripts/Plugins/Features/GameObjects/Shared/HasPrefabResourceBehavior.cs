using Assets.Scripts.Plugins.Features.GameObjects.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace Assets.Scripts.Plugins.Features.GameObjects.Shared
{
    public class HasPrefabResourceBehavior :
        BaseBehavior,
        IPrefabResourceBehavior
    {
        public string PrefabResourceId { get; set; }
    }
}

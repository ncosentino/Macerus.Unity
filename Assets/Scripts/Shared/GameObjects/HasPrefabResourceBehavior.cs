using Assets.Scripts.Api.GameObjects;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Assets.Scripts.Shared.GameObjects
{
    public class HasPrefabResourceBehavior :
        BaseBehavior,
        IPrefabResourceBehavior
    {
        public IIdentifier PrefabResourceId { get; set; }
    }
}

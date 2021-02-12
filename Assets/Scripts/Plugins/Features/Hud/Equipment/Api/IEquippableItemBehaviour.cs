using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment.Api
{
    public interface IEquippableItemBehaviour : IReadOnlyEquippableItemBehaviour
    {
        new ICanBeEquippedBehavior CanBeEquippedBehavior { get; set; }
    }
}
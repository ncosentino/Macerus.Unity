using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public interface IEquippableItemBehaviour : IReadOnlyEquippableItemBehaviour
    {
        new ICanBeEquippedBehavior CanBeEquippedBehavior { get; set; }
    }
}
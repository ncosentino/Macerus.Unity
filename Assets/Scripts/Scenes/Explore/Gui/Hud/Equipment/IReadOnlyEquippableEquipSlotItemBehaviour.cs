using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public interface IReadOnlyEquippableEquipSlotItemBehaviour : IReadOnlyEquippableItemBehaviour
    {
        ICanEquipBehavior CanEquipBehavior { get; }

        IIdentifier EquipSlotId { get; }
    }
}
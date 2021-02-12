using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment.Api
{
    public interface IReadOnlyEquippableEquipSlotItemBehaviour : IReadOnlyEquippableItemBehaviour
    {
        ICanEquipBehavior CanEquipBehavior { get; }

        IIdentifier EquipSlotId { get; }
    }
}
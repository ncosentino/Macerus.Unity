using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public interface IReadOnlyIconEquipmentSlotBehaviour
    {
        ICanEquipBehavior CanEquipBehavior { get; }

        IIdentifier TargetEquipSlotId { get; }
    }
}
using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment
{
    public interface IEquipmentSlotViewModel
    {
        IIdentifier EquipSlotId { get; }

        string PrefabResource { get; }

        string EmptyIconResource { get; }

        int X { get; }

        int Y { get; }
    }
}
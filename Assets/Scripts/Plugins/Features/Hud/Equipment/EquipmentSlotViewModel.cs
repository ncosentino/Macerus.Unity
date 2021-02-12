using Assets.Scripts.Plugins.Features.Hud.Equipment.Api;

using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment
{
    public sealed class EquipmentSlotViewModel : IEquipmentSlotViewModel
    {
        public EquipmentSlotViewModel(
            IIdentifier equipSlotId,
            string prefabResource,
            string emptyIconResource,
            int x,
            int y)
        {
            EquipSlotId = equipSlotId;
            PrefabResource = prefabResource;
            EmptyIconResource = emptyIconResource;
            X = x;
            Y = y;
        }

        public IIdentifier EquipSlotId { get; }

        public string PrefabResource { get; }

        public string EmptyIconResource { get; }

        public int X { get; }

        public int Y { get; }
    }
}
using Assets.Scripts.Plugins.Features.Hud.Equipment.Api;
using Assets.Scripts.Unity.Resources.Prefabs;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment
{
    // FIXME: this is currently not registered... what do?
    public sealed class EquippableEquipSlotItemBehaviourStitcher
    {
        public IReadOnlyEquippableEquipSlotItemBehaviour Attach(IEquipSlotPrefab equipSlot)
        {
            var equippableEquipSlotItemBehaviour = equipSlot.AddComponent<EquippableEquipSlotItemBehaviour>();
            // TODO: stitch the things
            return equippableEquipSlotItemBehaviour;
        }
    }
}
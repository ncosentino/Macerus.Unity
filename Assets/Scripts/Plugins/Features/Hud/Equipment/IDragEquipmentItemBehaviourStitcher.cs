namespace Assets.Scripts.Plugins.Features.Hud.Equipment
{
    public interface IDragEquipmentItemBehaviourStitcher
    {
        IReadOnlyDragEquipmentItemBehaviour Attach(IEquipSlotPrefab equipSlot);
    }
}
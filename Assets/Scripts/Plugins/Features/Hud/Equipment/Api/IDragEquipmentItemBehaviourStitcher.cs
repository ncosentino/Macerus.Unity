namespace Assets.Scripts.Plugins.Features.Hud.Equipment.Api
{
    public interface IDragEquipmentItemBehaviourStitcher
    {
        IReadOnlyDragEquipmentItemBehaviour Attach(IEquipSlotPrefab equipSlot);
    }
}
namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public interface IDragEquipmentItemBehaviourStitcher
    {
        IReadOnlyDragEquipmentItemBehaviour Attach(IEquipSlotPrefab equipSlot);
    }
}
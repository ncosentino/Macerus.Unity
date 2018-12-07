namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public interface IEquipmentSlotViewModel
    {
        string PrefabResource { get; }

        string EmptyIconResource { get; }

        int X { get; }

        int Y { get; }
    }
}
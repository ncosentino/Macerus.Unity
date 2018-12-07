namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public sealed class EquipmentSlotViewModel : IEquipmentSlotViewModel
    {
        public EquipmentSlotViewModel(
            string prefabResource,
            string emptyIconResource,
            int x,
            int y)
        {
            PrefabResource = prefabResource;
            EmptyIconResource = emptyIconResource;
            X = x;
            Y = y;
        }

        public string PrefabResource { get; }

        public string EmptyIconResource { get; }

        public int X { get; }

        public int Y { get; }
    }
}
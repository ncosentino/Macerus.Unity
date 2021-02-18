using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment.Api
{
    public interface IEquipmentItemDropUiFlow
    {
        void Execute(DroppedEventArgs droppedEventArgs);
    }
}
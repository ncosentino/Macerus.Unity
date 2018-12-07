using Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment;
using Assets.Scripts.Unity.GameObjects;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public interface IHasGuiEquipmentBehaviour : IReadonlyHasGuiEquipmentBehaviour
    {
        new IEquipmentSlotsFactory EquipmentSlotsFactory { get; set; }

        new IGameObjectManager GameObjectManager { get; set; }

        new IObjectDestroyer ObjectDestroyer { get; set; }
    }
}
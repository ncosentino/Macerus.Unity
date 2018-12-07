using Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment;
using Assets.Scripts.Unity.GameObjects;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public interface IReadonlyHasGuiEquipmentBehaviour
    {
        IEquipmentSlotsFactory EquipmentSlotsFactory { get; }

        IGameObjectManager GameObjectManager { get; }

        IObjectDestroyer ObjectDestroyer { get; }
    }
}
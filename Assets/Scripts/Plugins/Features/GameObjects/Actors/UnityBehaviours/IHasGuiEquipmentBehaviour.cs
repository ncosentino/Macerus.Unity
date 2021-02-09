using Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment;
using Assets.Scripts.Unity.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{
    public interface IHasGuiEquipmentBehaviour : IReadonlyHasGuiEquipmentBehaviour
    {
        new IEquipmentSlotsFactory EquipmentSlotsFactory { get; set; }

        new IGameObjectManager GameObjectManager { get; set; }

        new IObjectDestroyer ObjectDestroyer { get; set; }

        new IHasEquipmentBehavior HasEquipmentBehavior { get; set; }

        new ICanEquipBehavior CanEquipBehavior { get; set; }
    }
}
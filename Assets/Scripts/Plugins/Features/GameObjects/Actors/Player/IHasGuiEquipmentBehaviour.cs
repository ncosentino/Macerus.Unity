using Assets.Scripts.Plugins.Features.Hud.Equipment.Api;
using Assets.Scripts.Unity.GameObjects;

using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IHasGuiEquipmentBehaviour : IReadonlyHasGuiEquipmentBehaviour
    {
        new IEquipmentSlotsFactory EquipmentSlotsFactory { get; set; }

        new IUnityGameObjectManager GameObjectManager { get; set; }

        new IObjectDestroyer ObjectDestroyer { get; set; }

        new IHasEquipmentBehavior HasEquipmentBehavior { get; set; }

        new ICanEquipBehavior CanEquipBehavior { get; set; }
    }
}
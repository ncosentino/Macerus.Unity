using Assets.Scripts.Unity.Resources;
using UnityEngine.UI;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public interface IEquipSlotPrefab : IPrefab
    {
        Image ActiveIcon { get; }

        Image Background { get; }
    }
}
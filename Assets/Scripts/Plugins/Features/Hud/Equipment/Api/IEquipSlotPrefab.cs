using Assets.Scripts.Unity.Resources.Prefabs;

using UnityEngine.UI;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment.Api
{
    public interface IEquipSlotPrefab : IPrefab
    {
        Image ActiveIcon { get; }

        Image Background { get; }
    }
}
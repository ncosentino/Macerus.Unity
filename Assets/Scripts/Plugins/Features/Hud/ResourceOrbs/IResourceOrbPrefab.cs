using Assets.Scripts.Unity.Resources.Prefabs;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Plugins.Features.Hud.ResourceOrbs
{
    public interface IResourceOrbPrefab : IPrefab
    {
        GameObject OrbFill { get; }

        GameObject OrbMask { get; }

        GameObject Ornament { get; }

        Image OrbFillImage { get; }
    }
}

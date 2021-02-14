using Assets.Scripts.Unity.Resources.Prefabs;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Containers
{
    public interface IContainerPrefab : IPrefab
    {
        SpriteRenderer SpriteRenderer { get; }

        Collider2D Collision { get; }

        Collider2D Trigger { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Scripts.Unity;
using Assets.Scripts.Unity.GameObjects;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Plugins.Features.Hud.ResourceOrbs
{
    public sealed class ResourceOrbPrefab : IResourceOrbPrefab
    {
        private readonly Lazy<GameObject> _lazyOrnament;
        private readonly Lazy<GameObject> _lazyOrbMask;
        private readonly Lazy<GameObject> _lazyOrbFill;
        private readonly Lazy<Image> _lazyOrbFillImage;

        public ResourceOrbPrefab(GameObject gameObject)
        {
            GameObject = gameObject;
            _lazyOrnament = new Lazy<GameObject>(() => GameObject
                .GetChildGameObjects()
                .Single(x => x.name == "Ornament"));
            _lazyOrbMask = new Lazy<GameObject>(() => GameObject
                .GetChildGameObjects()
                .Single(x => x.name == "OrbMask"));
            _lazyOrbFill = new Lazy<GameObject>(() => OrbMask
                 .GetChildGameObjects()
                 .Single(x => x.name == "OrbFill"));
            _lazyOrbFillImage = new Lazy<Image>(() => OrbFill
                 .GetComponent<Image>());
        }

        public GameObject GameObject { get; }

        public GameObject Ornament => _lazyOrnament.Value;

        public GameObject OrbMask => _lazyOrbMask.Value;

        public GameObject OrbFill => _lazyOrbFill.Value;

        public Image OrbFillImage => _lazyOrbFillImage.Value;
    }
}

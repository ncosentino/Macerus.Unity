using System.Linq;
using Assets.Scripts.Scenes.Explore.GameObjects;
using Assets.Scripts.Unity.Resources;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors.Interceptors
{
    public sealed class LightRadiusBehaviorInterceptor : IGameObjectBehaviorInterceptor
    {
        private readonly IPrefabCreator _prefabCreator;

        public LightRadiusBehaviorInterceptor(IPrefabCreator prefabCreator)
        {
            _prefabCreator = prefabCreator;
        }

        public void Intercept(
            IGameObject gameObject, 
            GameObject unityGameObject)
        {
            var lightRadiusBehavior = gameObject
                .Get<ILightRadiusBehavior>()
                .SingleOrDefault();
            if (lightRadiusBehavior == null)
            {
                return;
            }

            var lightRadiusObject = _prefabCreator.Create<GameObject>("Mapping/Prefabs/LightRadius");
            lightRadiusObject.transform.position = new Vector3(0, 0, -1);
            lightRadiusObject.transform.SetParent(unityGameObject.transform, false);

            var lightComponent = lightRadiusObject.GetComponent<Light>();
            lightComponent.color = new Color(
                (float)lightRadiusBehavior.Red,
                (float)lightRadiusBehavior.Green,
                (float)lightRadiusBehavior.Blue);
            lightComponent.range = (float)lightRadiusBehavior.Radius;
            lightComponent.intensity = (float)lightRadiusBehavior.Intensity;
        }
    }
}
using System.Collections.Generic;

using Assets.Scripts.Unity.Resources.Prefabs;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Framework;

using UnityEditor;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class ImplicitEditorPrefabResourceIdConverter : 
        IDiscoverableBehaviorToBaseGameObjectConverter,
        IDiscoverableGameObjectToBehaviorConverter
    {
        private readonly IPrefabCreator _prefabCreator;

        public ImplicitEditorPrefabResourceIdConverter(IPrefabCreator prefabCreator)
        {
            _prefabCreator = prefabCreator;
        }

        public bool CanConvert(IBehavior behavior) => 
            behavior is IReadOnlyEditorPrefabResourceIdBehavior;

        public bool CanConvert(GameObject unityGameObject)
        {
            var objFromSource = PrefabUtility.GetCorrespondingObjectFromOriginalSource(unityGameObject);
            if (objFromSource == null)
            {
                return false;
            }

            var resourcePath = AssetDatabase.GetAssetPath(objFromSource);
            return !string.IsNullOrWhiteSpace(resourcePath);
        }

        GameObject IBehaviorToBaseGameObjectConverter.Convert(IBehavior behavior)
        {
            var castedBehavior = (IReadOnlyEditorPrefabResourceIdBehavior)behavior;
            var gameObject = _prefabCreator.Create<GameObject>(castedBehavior.PrefabResourceId.ToString());
            return gameObject;
        }

        GameObject IGameObjectToBehaviorConverter.Convert(IBehavior behavior) => null;

        public IEnumerable<IBehavior> Convert(GameObject unityGameObject)
        {
            var objFromSource = PrefabUtility.GetCorrespondingObjectFromOriginalSource(unityGameObject);
            var resourcePath = AssetDatabase.GetAssetPath(objFromSource);
            resourcePath = resourcePath
                .Substring(resourcePath.IndexOf("/Resources/") + "/Resources/".Length) // we need a relative path
                .Replace(".prefab", string.Empty); // we don't want the extension
            var resourceId = new StringIdentifier(resourcePath);
            var behavior = new EditorPrefabResourceIdBehavior(resourceId);
            yield return behavior;
        }
    }
}

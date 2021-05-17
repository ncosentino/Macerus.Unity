using System.Collections.Generic;

using Assets.Scripts.Unity.Resources;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Framework;

using UnityEditor;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class EditorPrefabResourceIdConverter : IDiscoverableBehaviorConverter
    {
        private readonly IResourceLoader _resourceLoader;

        public EditorPrefabResourceIdConverter(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
        }

        public bool CanConvert(IBehavior behavior) =>
            behavior is IReadOnlyEditorPrefabResourceIdBehavior;

        public bool CanConvert(Component component) =>
            component is EditorPrefabResourceBehaviour;

        public IEnumerable<Component> Convert(
            GameObject target,
            IBehavior behavior)
        {
            if (target.GetComponent<PrefabResourceBehaviour>() != null)
            {
                yield break;
            }

            var castedBehavior = (IReadOnlyEditorPrefabResourceIdBehavior)behavior;
            var component = target.AddComponent<EditorPrefabResourceBehaviour>();
            // NOTE: we don't want to instantiate a prefab here
            component.Prefab = _resourceLoader.Load<GameObject>(castedBehavior.PrefabResourceId.ToString());
            yield return component;
        }

        public IEnumerable<IBehavior> Convert(Component component)
        {
            var castedBehaviour = (EditorPrefabResourceBehaviour)component;
            UnityContracts.RequiresNotNull(
                castedBehaviour,
                castedBehaviour.Prefab,
                nameof(castedBehaviour.Prefab));
            var objFromSource = PrefabUtility.GetCorrespondingObjectFromOriginalSource(castedBehaviour.Prefab);
            UnityContracts.RequiresNotNull(
                castedBehaviour,
                objFromSource,
                nameof(PrefabUtility.GetCorrespondingObjectFromSource));
            var resourcePath = AssetDatabase.GetAssetPath(objFromSource);
            UnityContracts.RequiresNotNullOrWhitespace(
                castedBehaviour,
                resourcePath,
                nameof(AssetDatabase.GetAssetPath));

            resourcePath = resourcePath
                .Substring(resourcePath.IndexOf("/Resources/") + "/Resources/".Length) // we need a relative path
                .Replace(".prefab", string.Empty); // we don't want the extension
            var resourceId = new StringIdentifier(resourcePath);
            var behavior = new EditorPrefabResourceIdBehavior(resourceId);
            yield return behavior;
        }
    }
}

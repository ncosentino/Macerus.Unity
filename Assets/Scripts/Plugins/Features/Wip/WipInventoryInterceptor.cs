using System.Linq;
using Assets.Scripts.Scenes.Explore.GameObjects;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;
using UnityEngine;
using ILogger = ProjectXyz.Api.Logging.ILogger;

namespace Assets.Scripts.Plugins.Features.Wip
{
    public sealed class WipInventoryInterceptor : IGameObjectBehaviorInterceptor
    {
        private readonly IItemGeneratorFacade _itemGeneratorFacade;
        private readonly ILogger _logger;

        public WipInventoryInterceptor(
            IItemGeneratorFacade itemGeneratorFacade,
            ILogger logger)
        {
            _itemGeneratorFacade = itemGeneratorFacade;
            _logger = logger;
        }

        public void Intercept(
            IGameObject gameObject, 
            GameObject unityGameObject)
        {
            // FIXME: this is obviously a hack... how do we control which container to find?
            var targetContainerId = new StringIdentifier("Inventory");

            var itemContainerBehavior = gameObject
                .Get<IItemContainerBehavior>()
                .SingleOrDefault(x => targetContainerId.Equals(x.ContainerId));
            if (itemContainerBehavior == null)
            {
                return;
            }

            const int ITEMS_TO_GEN = 20;
            _logger.Debug($"'{this}' trying to generate {ITEMS_TO_GEN} item(s)...");
            var items = _itemGeneratorFacade
                .GenerateItems(new GeneratorContext(
                    ITEMS_TO_GEN,
                    ITEMS_TO_GEN,
                    new[]
                    {
                        new GeneratorAttribute(
                            new StringIdentifier("affix-type"),
                            new StringGeneratorAttributeValue("magic"),
                            true),
                        new GeneratorAttribute(
                            new StringIdentifier("item-level"),
                            new DoubleGeneratorAttributeValue(5),
                            true),
                    }))
                    .ToArray();

            foreach (var item in items)
            {
                _logger.Debug($"'{this}' trying to add '{item}' to '{itemContainerBehavior}'...");
                var wasAdded = itemContainerBehavior.TryAddItem(item);
                var wasAddedString = wasAdded ? "added" : "could not add";
                _logger.Debug($"'{this}' {wasAddedString} '{item}' to '{itemContainerBehavior}'.");
            }
        }
    }
}
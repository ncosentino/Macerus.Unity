using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Assets.Scripts.Autofac;
using Assets.Scripts.Plugins.Features.NewHud.Noesis.Resources;
using Assets.Scripts.Scenes.MainMenu.Gui.MainMenu;
using Autofac;
using Macerus.Api.Behaviors;
using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Encounters;
using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Api;
using Macerus.Plugins.Features.Inventory.Api;
using Macerus.Plugins.Features.MainMenu.Api;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;

namespace Assets.Blend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var macerusContainerBuilder = new MacerusContainerBuilder();
            var container = macerusContainerBuilder.CreateContainer();

            bool showMainMenu = false;
            if (showMainMenu)
            {
                Content = container.Resolve<IMainMenuView>();
                container.Resolve<IMainMenuController>().OpenMenu();
            }
            else
            {
                Content = container.Resolve<HudView>();

                var filterContextAmenity = container.Resolve<IFilterContextAmenity>();
                var filterContext = filterContextAmenity.CreateNoneFilterContext();
                var encounterManager = container.Resolve<IEncounterManager>();
                encounterManager.StartEncounter(
                    filterContext,
                    new StringIdentifier("test-encounter"));

                var dropTableId = new StringIdentifier("any_normal_magic_10x_lvl10");
                var lootGeneratorAmenity = container.Resolve<ILootGeneratorAmenity>();
                var generatedItems = lootGeneratorAmenity.GenerateLoot(dropTableId);

                var actorIdentifiers = container.Resolve<IMacerusActorIdentifiers>();
                var mapGameObjectManager = container.Resolve<IMapGameObjectManager>();
                var playerInventory = mapGameObjectManager
                    .GameObjects
                    .First(x => x.Has<IPlayerControlledBehavior>())
                    .Get<IItemContainerBehavior>()
                    .First(x => x.ContainerId.Equals(actorIdentifiers.InventoryIdentifier));
                foreach (var item in generatedItems)
                {
                    playerInventory.TryAddItem(item);
                }

                var controller = container.Resolve<IPlayerInventoryController>();
                controller.OpenInventory();
            }
        }
    }

    public sealed class Item : ProjectXyz.Api.GameObjects.IGameObject
    {
        public IReadOnlyCollection<IBehavior> Behaviors { get; } = new IBehavior[] 
        {
            new IdentifierBehavior(new StringIdentifier(Guid.NewGuid().ToString())),
        };
    }
}

using System;
using System.Linq;
using System.Threading;
using System.Windows;
using Assets.Scripts.Autofac;
using Assets.Scripts.Plugins.Features.NewHud.Noesis;
using Assets.Scripts.Scenes.MainMenu.Gui.MainMenu;
using Autofac;
using Macerus.Api.Behaviors;
using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.CharacterSheet.Api;
using Macerus.Plugins.Features.Encounters;
using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.GameObjects.Actors.Generation;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Api;
using Macerus.Plugins.Features.GameObjects.Skills.Api;
using Macerus.Plugins.Features.Gui.Api.SceneTransitions;
using Macerus.Plugins.Features.Inventory.Api;
using Macerus.Plugins.Features.Inventory.Api.Crafting;
using Macerus.Plugins.Features.MainMenu.Api;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
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

            var transitionController = container.Resolve<ITransitionController>();
            transitionController.StartTransition(
                TimeSpan.FromSeconds(0),
                TimeSpan.FromSeconds(0.5),
                () => { },
                () => { });

            bool showMainMenu = false;
            if (showMainMenu)
            {
                Content = container.Resolve<IMainMenuView>();
                var mainMenuController = container.Resolve<IMainMenuController>();
                container.Resolve<ITransitionController>().StartTransition(
                    TimeSpan.Zero,
                    TimeSpan.FromSeconds(3),
                    () => mainMenuController.OpenMenu(),
                    () => { });
            }
            else
            {
                Content = container.Resolve<IHudView>();

                var mapManager = container.Resolve<IMapManager>();

                var mapGameObjectManager = container.Resolve<IMapGameObjectManager>();
                mapGameObjectManager.MarkForAddition(CreatePlayerInstance(container));
                mapGameObjectManager.Synchronize();

                var filterContextAmenity = container.Resolve<IFilterContextAmenity>();
                var filterContext = filterContextAmenity.CreateNoneFilterContext();
                var encounterManager = container.Resolve<IEncounterManager>();
                encounterManager
                    .StartEncounterAsync(
                        filterContext,
                        new StringIdentifier("test-encounter"))
                    .Wait();

                var dropTableId = new StringIdentifier("any_normal_magic_rare_10x_lvl10");
                var lootGeneratorAmenity = container.Resolve<ILootGeneratorAmenity>();
                var generatedItems = lootGeneratorAmenity.GenerateLoot(dropTableId);

                var actorIdentifiers = container.Resolve<IMacerusActorIdentifiers>();
                var playerInventory = mapGameObjectManager
                    .GameObjects
                    .First(x => x.Has<IPlayerControlledBehavior>())
                    .Get<IItemContainerBehavior>()
                    .First(x => x.ContainerId.Equals(actorIdentifiers.InventoryIdentifier));
                foreach (var item in generatedItems)
                {
                    playerInventory.TryAddItem(item);
                }

                var skillAmenity = container.Resolve<ISkillAmenity>();
                var skillsBehavior = mapGameObjectManager
                    .GameObjects
                    .First(x => x.Has<IPlayerControlledBehavior>())
                    .GetOnly<IHasSkillsBehavior>();

                var inventoryController = container.Resolve<IPlayerInventoryController>();
                inventoryController.OpenInventory();

                //var characterSheetController = container.Resolve<ICharacterSheetController>();
                //characterSheetController.OpenCharacterSheet();

                var craftingController = container.Resolve<ICraftingController>();
                craftingController.OpenCraftingWindow();
            }

            container.Resolve<IAsyncGameEngine>().RunAsync(CancellationToken.None);
        }

        // FIXME: this is just a hack to force player creation
        private IGameObject CreatePlayerInstance(IContainer container)
        {
            var filterContextAmenity = container.Resolve<IFilterContextAmenity>();
            var gameObjectIdentifiers = container.Resolve<IGameObjectIdentifiers>();
            var actorIdentifiers = container.Resolve<IMacerusActorIdentifiers>();
            var actorGeneratorFacade = container.Resolve<IActorGeneratorFacade>();
            var context = filterContextAmenity.CreateFilterContextForSingle(
                filterContextAmenity.CreateRequiredAttribute(
                    gameObjectIdentifiers.FilterContextTypeId,
                    actorIdentifiers.ActorTypeIdentifier),
                filterContextAmenity.CreateRequiredAttribute(
                    actorIdentifiers.ActorDefinitionIdentifier,
                    new StringIdentifier("player")));
            var player = actorGeneratorFacade
                .GenerateActors(
                    context,
                    new IGeneratorComponent[] { })
                .Single();
            return player;
        }
    }
}

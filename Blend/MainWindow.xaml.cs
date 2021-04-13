using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Assets.Scripts.Autofac;
using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Plugins.Features.Inventory.Noesis;
using Assets.Scripts.Plugins.Features.Inventory.Noesis.Resources;
using Autofac;
using Macerus.Api.Behaviors;
using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Animations.Lpc;
using Macerus.Plugins.Features.Encounters;
using Macerus.Plugins.Features.Inventory.Api;
using Macerus.Plugins.Features.Inventory.Default;
using Macerus.Plugins.Features.Mapping.TiledNet;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
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

            var resourceImageSourceFactory = container.Resolve<IResourceImageSourceFactory>();
            var equipmentSlotToNoesisConverter = new EquipmentSlotToNoesisViewModelConverter(resourceImageSourceFactory);
            var noesisEquipmentViewModel = new ItemSlotCollectionNoesisViewModel(
                equipmentSlotToNoesisConverter,
                container.ResolveNamed<IItemSlotCollectionViewModel>("player equipment"),
                null);

            var bagSlotToNoesisConverter = new BagSlotToNoesisViewModelConverter(resourceImageSourceFactory);
            var equipment = new InventoryEquipmentView(noesisEquipmentViewModel);
            var noesisBagViewModel = new ItemSlotCollectionNoesisViewModel(
                bagSlotToNoesisConverter,
                container.ResolveNamed<IItemSlotCollectionViewModel>("player bag"),
                null);
            var bag = new InventoryBagView(noesisBagViewModel);

            var itemDragViewModel = container.Resolve<IItemDragViewModel>();
            var noesisItemDragViewModel = new ItemDragNoesisViewModel(
                bagSlotToNoesisConverter,
                itemDragViewModel);
            var playerInventoryWindow = new PlayerInventoryWindow(
                container.Resolve<IViewWelderFactory>(),
                noesisItemDragViewModel,
                equipment,
                bag);

            Content.Children.Add(playerInventoryWindow);

            var filterContextAmenity = container.Resolve<IFilterContextAmenity>();
            var filterContext = filterContextAmenity.CreateNoneFilterContext();
            var encounterManager = container.Resolve<IEncounterManager>();
            encounterManager.StartEncounter(
                filterContext,
                new StringIdentifier("test-encounter"));

            var dropTableRepository = container.Resolve<IDropTableRepositoryFacade>();
            var dropTableIdentifiers = container.Resolve<IDropTableIdentifiers>();
            var dropTableId = new StringIdentifier("any_normal_magic_10x_lvl10");
            var dropTable = dropTableRepository.GetForDropTableId(dropTableId);
            var dropTableAttribute = filterContextAmenity.CreateRequiredAttribute(
                dropTableIdentifiers.FilterContextDropTableIdentifier,
                dropTable.DropTableId);

            var lootFilterContext = filterContextAmenity
                .CreateNoneFilterContext()
                .WithAdditionalAttributes(new[] { dropTableAttribute })
                .WithRange(
                    dropTable.MinimumGenerateCount,
                    dropTable.MaximumGenerateCount);

            var lootGenerator = container.Resolve<ILootGenerator>();
            var generatedItems = lootGenerator.GenerateLoot(lootFilterContext);

            var mapGameObjectManager = container.Resolve<IMapGameObjectManager>();
            var playerInventory = mapGameObjectManager
                .GameObjects
                .First(x => x.Has<IPlayerControlledBehavior>())
                .Get<IItemContainerBehavior>()
                .First(x => x.ContainerId.Equals(new StringIdentifier("Inventory")));
            foreach (var item in generatedItems)
            {
                playerInventory.TryAddItem(item);
            }

            var controller = container.Resolve<IPlayerInventoryController>();
            controller.OpenInventory();
        }
    }

    public sealed class Item : ProjectXyz.Api.GameObjects.IGameObject
    {
        public IReadOnlyCollection<IBehavior> Behaviors { get; } = new IBehavior[] 
        {
            new IdentifierBehavior(new StringIdentifier(Guid.NewGuid().ToString())),
        };
    }

    public sealed class BlendModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<ConsoleLogger>()
                .AsImplementedInterfaces();
            builder
                .RegisterType<NoneLpcAnimationDiscovererSettings>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MappingAssetPaths>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapResourceLoader>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private sealed class ConsoleLogger : ILogger
        {
            public void Debug(string message) => Debug(message, null);

            public void Debug(string message, object data)
            {
                System.Diagnostics.Debug.WriteLine($"DEBUG: {message}");
                if (data != null)
                {
                    System.Diagnostics.Debug.WriteLine($"\t{data}");
                }
            }

            public void Error(string message)
            {
                throw new NotImplementedException();
            }

            public void Error(string message, object data)
            {
                throw new NotImplementedException();
            }

            public void Info(string message)
            {
                throw new NotImplementedException();
            }

            public void Info(string message, object data)
            {
                throw new NotImplementedException();
            }

            public void Warn(string message) => Warn(message, null);

            public void Warn(string message, object data)
            {
                System.Diagnostics.Debug.WriteLine($"WARN: {message}");
                if (data != null)
                {
                    System.Diagnostics.Debug.WriteLine($"\t{data}");
                }
            }
        }

        public sealed class MappingAssetPaths : IMappingAssetPaths
        {
            private readonly Lazy<DirectoryInfo> _lazyResourceRoot;
            private readonly Lazy<DirectoryInfo> _lazyMapsRoot;

            public MappingAssetPaths()
            {
                _lazyResourceRoot =
                   new Lazy<DirectoryInfo>(() =>
                   {
                       return new DirectoryInfo(@"..\..\..\Assets\Resources");
                   });
                _lazyMapsRoot =
                    new Lazy<DirectoryInfo>(() =>
                    {
                        return new DirectoryInfo(Path.Combine(_lazyResourceRoot.Value.FullName, @"Mapping\Maps"));
                    });
            }

            public string MapsRoot => _lazyMapsRoot.Value.FullName;

            public string ResourcesRoot => _lazyResourceRoot.Value.FullName;
        }

        public sealed class MapResourceLoader : ITiledMapResourceLoader
        {
            private readonly IMappingAssetPaths _mappingAssetPaths;

            public MapResourceLoader(IMappingAssetPaths mappingAssetPaths)
            {
                _mappingAssetPaths = mappingAssetPaths;
            }

            public Stream LoadStream(string pathToResource)
            {
                var fullPath = Path.Combine(
                    _mappingAssetPaths.ResourcesRoot,
                    $"{pathToResource}.txt");
                return File.OpenRead(fullPath);
            }
        }
    }
}

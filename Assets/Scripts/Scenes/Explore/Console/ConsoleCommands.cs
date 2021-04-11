using System.Linq;

using Assets.Scripts;
using Assets.Scripts.Behaviours;
using Assets.Scripts.Plugins.Features.Console;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.GameObjects;

using Autofac;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Encounters;
using Macerus.Plugins.Features.GameObjects.Skills.Api;
using Macerus.Plugins.Features.Weather;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Enchantments.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.TurnBased.Api;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Console
{
    public sealed class ConsoleCommandsBehaviour : MonoBehaviour
    {
        private ProjectXyz.Api.Logging.ILogger _logger;
        private IUnityGameObjectManager _unityGameObjectManager;
        private IReadOnlyMapGameObjectManager _mapGameObjectManager;
        private ISkillAmenity _skillAmenity;
        private IReadOnlyStatDefinitionToTermMappingRepository _statDefinitionToTermMappingRepository;
        private IStatCalculationService _statCalculationService;
        private IWeatherAmenity _weatherAmenity;
        private IWeatherManager _weatherManager;
        private ITurnBasedManager _turnBasedManager;
        private IMapFormatter _mapFormatter;
        private IEncounterManager _encounterManager;
        private IFilterContextProvider _filterContextProvider;

        private void Start()
        {
            // NOTE: we're violating the stitcher pattern here and *YES* using 
            // the most evil service locator pattern here BUT here me out...
            // This class will probably be mostly for test methods and this is 
            // the easiest shortcut for getting access to all kinds of stuff
            // without making separate classes for stitching etc...
            var container = GameDependencyBehaviour.Container;

            _logger = container.Resolve<ProjectXyz.Api.Logging.ILogger>();
            _mapGameObjectManager = container.Resolve<IReadOnlyMapGameObjectManager>();
            _unityGameObjectManager = container.Resolve<IUnityGameObjectManager>();
            _skillAmenity = container.Resolve<ISkillAmenity>();
            _statDefinitionToTermMappingRepository = container.Resolve<IReadOnlyStatDefinitionToTermMappingRepository>();
            _statCalculationService = container.Resolve<IStatCalculationService>();
            _weatherAmenity = container.Resolve<IWeatherAmenity>();
            _weatherManager = container.Resolve<IWeatherManager>();
            _turnBasedManager = container.Resolve<ITurnBasedManager>();
            _mapFormatter = container.Resolve<IMapFormatter>();
            _encounterManager = container.Resolve<IEncounterManager>();
            _filterContextProvider = container.Resolve<IFilterContextProvider>();

            container
                .Resolve<IConsoleCommandRegistrar>()
                .RegisterDiscoverableCommandsFromInstance(this);
        }

        [DiscoverableConsoleCommand("Starts an encounter with the specified ID.")]
        private void EncounterStart(string encounterId)
        {
            var filterContext = _filterContextProvider.GetContext();
            _encounterManager.StartEncounter(
                filterContext,
                new StringIdentifier(encounterId));
        }

        [DiscoverableConsoleCommand("Toggles the map grid lines on or off.")]
        private void MapGridLinesToggle(bool enabled)
        {
            _mapFormatter.ToggleGridLines(enabled);
        }

        [DiscoverableConsoleCommand("Gets the location of the player.")]
        private void PlayerGetLocation()
        {
            var worldLocationBehavior = _mapGameObjectManager
                .GameObjects
                .Single(x => x.Has<IPlayerControlledBehavior>())
                .GetOnly<IWorldLocationBehavior>();
            _logger.Info(
                $"({worldLocationBehavior.X},{worldLocationBehavior.Y})");
        }

        [DiscoverableConsoleCommand("Sets the location of the player.")]
        private void PlayerSetLocation(double x, double y)
        {
            var worldLocationBehavior = _mapGameObjectManager
                .GameObjects
                .Single(x => x.Has<IPlayerControlledBehavior>())
                .GetOnly<IWorldLocationBehavior>();
            var unityPlayerObject = _unityGameObjectManager
                .FindAll(x => x.IsPlayerControlled())
                .Single();

            worldLocationBehavior.SetLocation(x, y);
            unityPlayerObject.transform.position = new Vector3(
                (float)x,
                (float)y,
                unityPlayerObject.transform.position.z);
            _logger.Info(
                $"Set player location to ({worldLocationBehavior.X},{worldLocationBehavior.Y}).");
        }

        [DiscoverableConsoleCommand("Gets the size of the player.")]
        private void PlayerGetSize()
        {
            var worldLocationBehavior = _mapGameObjectManager
                .GameObjects
                .Single(x => x.Has<IPlayerControlledBehavior>())
                .GetOnly<IWorldLocationBehavior>();
            _logger.Info(
                $"({worldLocationBehavior.Width},{worldLocationBehavior.Height})");
        }

        [DiscoverableConsoleCommand("Sets whether or not to use global syncing for the turn based manager.")]
        private void TurnBasedManagerSetGlobalSync(bool value)
        {
            _turnBasedManager.GlobalSync = value;
        }

        [DiscoverableConsoleCommand("Sets whether or not to clear the applicable objects on update for the turn based manager.")]
        private void TurnBasedManagerSetClearApplicableOnUpdate(bool value)
        {
            _turnBasedManager.ClearApplicableOnUpdate = value;
        }

        [DiscoverableConsoleCommand("Sets whether or not to sync turns from elapsed time for the turn based manager.")]
        private void TurnBasedManagerSetSyncTurnsFromElapsedTime(bool value)
        {
            _turnBasedManager.SyncTurnsFromElapsedTime = value;
        }

        [DiscoverableConsoleCommand("Gets the properties for the turn based manager.")]
        private void TurnBasedManagerGetPoperties()
        {
            _logger.Info(
                $"Turn Based Manager:\r\n" +
                $"\t{nameof(_turnBasedManager.ClearApplicableOnUpdate)}: {_turnBasedManager.ClearApplicableOnUpdate}\r\n" +
                $"\t{nameof(_turnBasedManager.GlobalSync)}: {_turnBasedManager.GlobalSync}\r\n" +
                $"\t{nameof(_turnBasedManager.SyncTurnsFromElapsedTime)}: {_turnBasedManager.SyncTurnsFromElapsedTime}\r\n" +
                $"\t{nameof(_turnBasedManager.ApplicableGameObjects)}:\r\n" +
                $"\t\t{string.Join("\r\n\t\t", _turnBasedManager.ApplicableGameObjects)}");
        }

        [DiscoverableConsoleCommand("Sets the weather table to one with the specified ID.")]
        private void WeatherSetTable(string rawWeatherTableId)
        {
            var weatherTableId = new StringIdentifier(rawWeatherTableId);
            var weatherTable = _weatherAmenity.GetWeatherTableById(weatherTableId);
            if (weatherTable == null)
            {
                _logger.Warn(
                    $"There was no weather table with ID '{rawWeatherTableId}'.");
                return;
            }

            _weatherManager.WeatherTable = weatherTable;
        }

        [DiscoverableConsoleCommand("Gets a stat value with the specified ID (or term) from the player.")]
        private void PlayerGetStat(string rawStatDefinitionId)
        {
            var player = _mapGameObjectManager
                .GameObjects
                .Single(x => x.Has<IPlayerControlledBehavior>());

            IIdentifier statDefinitionId;
            if (int.TryParse(rawStatDefinitionId, out var intStatDefinitionId))
            {
                statDefinitionId = new IntIdentifier(intStatDefinitionId);
            }
            else
            {
                statDefinitionId = _statDefinitionToTermMappingRepository
                    .GetStatDefinitionToTermMappingByTerm(rawStatDefinitionId)
                    ?.StatDefinitionId;
                if (statDefinitionId == null)
                {
                    _logger.Warn($"Could not find a stat definition by term '{rawStatDefinitionId}'.");
                    return;
                }
            }

            var context = new StatCalculationContext(
                new IComponent[] { },
                new IEnchantment[] { });
            var value = _statCalculationService.GetStatValue(
                player,
                statDefinitionId,
                context);
            _logger.Info($"{value}");
        }

        [DiscoverableConsoleCommand("Sets a base stat value with the specified ID (or term) and valu on the player.")]
        private void PlayerSetBaseStat(string rawStatDefinitionId, double value)
        {
            var player = _mapGameObjectManager
                .GameObjects
                .Single(x => x.Has<IPlayerControlledBehavior>());

            IIdentifier statDefinitionId;
            if (int.TryParse(rawStatDefinitionId, out var intStatDefinitionId))
            {
                statDefinitionId = new IntIdentifier(intStatDefinitionId);
            }
            else
            {
                statDefinitionId = _statDefinitionToTermMappingRepository
                    .GetStatDefinitionToTermMappingByTerm(rawStatDefinitionId)
                    ?.StatDefinitionId;
                if (statDefinitionId == null)
                {
                    _logger.Warn($"Could not find a stat definition by term '{rawStatDefinitionId}'.");
                    return;
                }
            }

            player
                .GetOnly<IHasMutableStatsBehavior>()
                .MutateStats(stats => stats[statDefinitionId] = value);
        }

        [DiscoverableConsoleCommand("Adds a skill with the specified ID to the player.")]
        private void PlayerAddSkill(string skillId)
        {
            var player = _mapGameObjectManager
                .GameObjects
                .Single(x => x.Has<IPlayerControlledBehavior>());

            var skill = _skillAmenity.GetSkillById(new StringIdentifier(skillId));
            if (skill == null)
            {
                _logger.Warn($"Could not find a skill with ID '{skillId}'.");
                return;
            }

            var hasSkillsbehavior = player.GetOnly<IHasSkillsBehavior>();
            hasSkillsbehavior.Add(new[] { skill });
        }
    }
}

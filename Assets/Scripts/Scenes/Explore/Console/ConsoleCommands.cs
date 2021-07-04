using System;
using System.Linq;

using Assets.Scripts;
using Assets.Scripts.Behaviours;
using Assets.Scripts.Behaviours.Generic;
using Assets.Scripts.Plugins.Features.Console;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.GameObjects;

using Autofac;

using Macerus.Plugins.Features.Encounters;
using Macerus.Plugins.Features.GameObjects.Skills.Api;
using Macerus.Plugins.Features.Mapping;
using Macerus.Plugins.Features.Stats.Api;
using Macerus.Plugins.Features.Weather;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Plugins.Features.PartyManagement;
using ProjectXyz.Plugins.Features.TurnBased;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Console
{
    public sealed class ConsoleCommandsBehaviour : MonoBehaviour
    {
        private ProjectXyz.Api.Logging.ILogger _logger;
        private IUnityGameObjectManager _unityGameObjectManager;
        private IMapGameObjectManager _mapGameObjectManager;
        private IMappingAmenity _mappingAmenity;
        private ISkillAmenity _skillAmenity;
        private IReadOnlyStatDefinitionToTermMappingRepository _statDefinitionToTermMappingRepository;
        private IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private IWeatherAmenity _weatherAmenity;
        private IWeatherManager _weatherManager;
        private ITurnBasedManager _turnBasedManager;
        private IMapFormatter _mapFormatter;
        private IEncounterManager _encounterManager;
        private IFilterContextProvider _filterContextProvider;
        private IMapProvider _mapProvider;
        private ITimeProvider _timeProvider;
        private IObjectDestroyer _objectDestroyer;
        private ITileMarkerFactory _tileMarkerFactory;
        private IRosterManager _rosterManager;

        private void Start()
        {
            // NOTE: we're violating the stitcher pattern here and *YES* using 
            // the most evil service locator pattern here BUT here me out...
            // This class will probably be mostly for test methods and this is 
            // the easiest shortcut for getting access to all kinds of stuff
            // without making separate classes for stitching etc...
            var container = GameDependencyBehaviour.Container;

            _logger = container.Resolve<ProjectXyz.Api.Logging.ILogger>();
            _mapGameObjectManager = container.Resolve<IMapGameObjectManager>();
            _mappingAmenity = container.Resolve<IMappingAmenity>();
            _unityGameObjectManager = container.Resolve<IUnityGameObjectManager>();
            _skillAmenity = container.Resolve<ISkillAmenity>();
            _statDefinitionToTermMappingRepository = container.Resolve<IReadOnlyStatDefinitionToTermMappingRepository>();
            _statCalculationServiceAmenity = container.Resolve<IStatCalculationServiceAmenity>();
            _weatherAmenity = container.Resolve<IWeatherAmenity>();
            _weatherManager = container.Resolve<IWeatherManager>();
            _turnBasedManager = container.Resolve<ITurnBasedManager>();
            _mapFormatter = container.Resolve<IMapFormatter>();
            _encounterManager = container.Resolve<IEncounterManager>();
            _filterContextProvider = container.Resolve<IFilterContextProvider>();
            _mapProvider = container.Resolve<IMapProvider>();
            _objectDestroyer = container.Resolve<IObjectDestroyer>();
            _timeProvider = container.Resolve<ITimeProvider>();
            _tileMarkerFactory = container.Resolve<ITileMarkerFactory>();
            _rosterManager = container.Resolve<IRosterManager>();

            container
                .Resolve<IConsoleCommandRegistrar>()
                .RegisterDiscoverableCommandsFromInstance(this);
        }

        [DiscoverableConsoleCommand("Prints the path between two points.")]
        private async void PathBetweenPoints(double startX, double startY, double endX, double endY, bool includeDiagonals)
        {
            var path = await _mapProvider
                .PathFinder
                .FindPathAsync(
                    new System.Numerics.Vector2((float)startX, (float)startY),
                    new System.Numerics.Vector2((float)endX, (float)endY),
                    new System.Numerics.Vector2(1, 1),
                    includeDiagonals)
                .ConfigureAwait(false);

            var debugVisualPath = new GameObject();
            debugVisualPath.name = $"Path from ({startX},{startY}) to ({endX},{endY})";
            var debugVisualPathBehaviour = debugVisualPath.AddComponent<DebugVisualPathBehaviour>();
            debugVisualPathBehaviour.Points = path.Positions;
            var selfDestructBehavior = debugVisualPath.AddComponent<SelfDestructBehaviour>();
            selfDestructBehavior.ObjectDestroyer = _objectDestroyer;
            selfDestructBehavior.TimeProvider = _timeProvider;
            selfDestructBehavior.Duration = TimeSpan.FromSeconds(10);

            _logger.Info(
                $"Path between ({startX},{startY}) and ({endX},{endY}):\r\n" +
                $"{string.Join("\r\n", path.Positions.Select(p => $"\t({p.X},{p.Y})"))}");
        }

        [DiscoverableConsoleCommand("Prints the path between two points.")]
        private void MapMarkerCreate(float x, float y, float r, float g, float b, float a, string name, double seconds)
        {
            _tileMarkerFactory.CreateTileMarker(
                name,
                new Vector3(x, y),
                new Color(r, g, b, a),
                seconds <= 0 ? (TimeSpan?)null : TimeSpan.FromSeconds(seconds));
        }

        [DiscoverableConsoleCommand("Prints free adjacent points to an object with the specified ID.")]
        private void AdjacentPointsToGameObjectFree(string idAsString)
        {
            var objId = new StringIdentifier(idAsString);
            var matchingObj = _mapGameObjectManager
                .GameObjects
                .FirstOrDefault(x => x.GetOnly<IIdentifierBehavior>().Id.Equals(objId));
            if (matchingObj == null)
            {
                _logger.Warn($"No object found with ID '{objId}'.");
                return;
            }

            var positionBehavior = matchingObj.GetOnly<IReadOnlyPositionBehavior>();
            var sizeBehavior = matchingObj.GetOnly<IReadOnlySizeBehavior>();
            var size = new System.Numerics.Vector2(
                (float)sizeBehavior.Width,
                (float)sizeBehavior.Height);
            var position = new System.Numerics.Vector2(
                (float)positionBehavior.X,
                (float)positionBehavior.Y);
            
            var adjacentPositions = _mapProvider.PathFinder.GetFreeAdjacentPositionsToObject(
                position,
                size,
                true);
            _logger.Info(
                $"'{objId}' at position ({position.X},{position.Y}) has " +
                $"adjacent points:\r\n" +
                $"{string.Join("\r\n", adjacentPositions.Select(p => $"\t({p.X},{p.Y})"))}");

            foreach (var adjacentPosition in adjacentPositions)
            {
                _tileMarkerFactory.CreateTileMarker(
                    $"Adjacent Position Marker ({adjacentPosition.X},{adjacentPosition.Y})",
                    new Vector3(adjacentPosition.X, adjacentPosition.Y),
                    new Color(1, 0, 0, 0.5f),
                    TimeSpan.FromSeconds(3));
            }
        }

        [DiscoverableConsoleCommand("Prints allowed destination paths for a game object.")]
        private void PathPossibleDestinations(string idAsString, int distance, bool includeDiagonals)
        {
            var objId = new StringIdentifier(idAsString);
            var matchingObj = _mapGameObjectManager
                .GameObjects
                .FirstOrDefault(x => x.GetOnly<IIdentifierBehavior>().Id.Equals(objId));
            if (matchingObj == null)
            {
                _logger.Warn($"No object found with ID '{objId}'.");
                return;
            }

            var positionBehavior = matchingObj.GetOnly<IReadOnlyPositionBehavior>();
            var sizeBehavior = matchingObj.GetOnly<IReadOnlySizeBehavior>();
            var size = new System.Numerics.Vector2(
                (float)sizeBehavior.Width,
                (float)sizeBehavior.Height);
            var position = new System.Numerics.Vector2(
                (float)positionBehavior.X,
                (float)positionBehavior.Y);

            var freeTiles = _mapProvider.PathFinder.GetAllowedPathDestinations(
                position,
                size,
                distance,
                includeDiagonals);

            foreach (var freeTile in freeTiles)
            {
                _tileMarkerFactory.CreateTileMarker(
                    $"Allowed Destination ({freeTile.X},{freeTile.Y})",
                    new Vector3(freeTile.X, freeTile.Y),
                    new Color(0, 1, 0, 0.5f),
                    TimeSpan.FromSeconds(3));
            }
        }

        [DiscoverableConsoleCommand("Prints all adjacent points to an object with the specified ID.")]
        private void AdjacentPointsToGameObjectAll(string idAsString)
        {
            var objId = new StringIdentifier(idAsString);
            var matchingObj = _mapGameObjectManager
                .GameObjects
                .FirstOrDefault(x => x.GetOnly<IIdentifierBehavior>().Id.Equals(objId));
            if (matchingObj == null)
            {
                _logger.Warn($"No object found with ID '{objId}'.");
                return;
            }

            var positionBehavior = matchingObj.GetOnly<IReadOnlyPositionBehavior>();
            var sizeBehavior = matchingObj.GetOnly<IReadOnlySizeBehavior>();
            var size = new System.Numerics.Vector2(
                (float)sizeBehavior.Width,
                (float)sizeBehavior.Height);
            var position = new System.Numerics.Vector2(
                (float)positionBehavior.X,
                (float)positionBehavior.Y);

            var adjacentPositions = _mapProvider.PathFinder.GetAllAdjacentPositionsToObject(
                position,
                size,
                true);
            _logger.Info(
                $"'{objId}' at position ({position.X},{position.Y}) has " +
                $"adjacent points:\r\n" +
                $"{string.Join("\r\n", adjacentPositions.Select(p => $"\t({p.X},{p.Y})"))}");

            foreach (var adjacentPosition in adjacentPositions)
            {
                _tileMarkerFactory.CreateTileMarker(
                    $"Adjacent Position Marker ({adjacentPosition.X},{adjacentPosition.Y})",
                    new Vector3(adjacentPosition.X, adjacentPosition.Y),
                    new Color(1, 0, 0, 0.5f),
                    TimeSpan.FromSeconds(3));
            }
        }

        [DiscoverableConsoleCommand("Starts an encounter with the specified ID.")]
        private async void EncounterStart(string encounterId)
        {
            var filterContext = _filterContextProvider.GetContext();
            await _encounterManager
                .StartEncounterAsync(
                    filterContext,
                    new StringIdentifier(encounterId))
                .ConfigureAwait(false);
        }

        [DiscoverableConsoleCommand("Toggles the map grid lines on or off.")]
        private void MapGridLinesToggle(bool enabled)
        {
            _mapFormatter.ToggleGridLines(enabled);
        }

        [DiscoverableConsoleCommand("Clears the map game objects.")]
        private void MapGameObjectsClear()
        {
            _mapGameObjectManager.MarkForRemoval(_mapGameObjectManager.GameObjects.ToArray());
            _mapGameObjectManager.Synchronize();
        }

        [DiscoverableConsoleCommand("Removes the map game object with the specified id.")]
        private void MapGameObjectsRemoveById(string idAsString)
        {
            var objId = new StringIdentifier(idAsString);
            var matchingObj = _mapGameObjectManager
                .GameObjects
                .FirstOrDefault(x => x.GetOnly<IIdentifierBehavior>().Id.Equals(objId));
            if (matchingObj == null)
            {
                _logger.Warn($"No object found with ID '{objId}'.");
                return;
            }

            _mapGameObjectManager.MarkForRemoval(matchingObj);
            _mapGameObjectManager.Synchronize();
        }

        [DiscoverableConsoleCommand("Gets the position of the player.")]
        private void PlayerGetPosition()
        {
            var positionBehavior = _rosterManager
                .CurrentlyControlledActor
                .GetOnly<IReadOnlyPositionBehavior>();
            _logger.Info(
                $"({positionBehavior.X},{positionBehavior.Y})");
        }

        [DiscoverableConsoleCommand("Sets the location of the player.")]
        private void PlayerSetPosition(double x, double y)
        {
            var player = _rosterManager.CurrentlyControlledActor;
            var positionBehavior = player.GetOnly<IPositionBehavior>();
            var unityPlayerObject = _unityGameObjectManager
                .FindAll(x => x.GetGameObject() == player)
                .Single();

            positionBehavior.SetPosition(x, y);
            unityPlayerObject.transform.position = new Vector3(
                (float)x,
                (float)y,
                unityPlayerObject.transform.position.z);
            _logger.Info(
                $"Set player location to ({positionBehavior.X},{positionBehavior.Y}).");
        }

        [DiscoverableConsoleCommand("Gets the size of the player.")]
        private void PlayerGetSize()
        {
            var sizeBehavior = _rosterManager
                .CurrentlyControlledActor
                .GetOnly<IReadOnlySizeBehavior>();
            _logger.Info(
                $"({sizeBehavior.Width},{sizeBehavior.Height})");
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
                $"\t{nameof(_turnBasedManager.GetApplicableGameObjects)}:\r\n" +
                $"\t\t{string.Join("\r\n\t\t", _turnBasedManager.GetApplicableGameObjects())}");
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
            var player = _rosterManager.CurrentlyControlledActor;

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

            var value = _statCalculationServiceAmenity.GetStatValue(
                player,
                statDefinitionId);
            _logger.Info($"{value}");
        }

        [DiscoverableConsoleCommand("Sets a base stat value with the specified ID (or term) and valu on the player.")]
        private void PlayerSetBaseStat(string rawStatDefinitionId, double value)
        {
            var player = _rosterManager.CurrentlyControlledActor;

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
            var player = _rosterManager.CurrentlyControlledActor;

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

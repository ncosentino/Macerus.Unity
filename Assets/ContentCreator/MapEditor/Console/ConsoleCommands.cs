using System.Linq;

using Assets.Scripts.Behaviours;
using Assets.Scripts.Plugins.Features.Console;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.GameObjects;

using Autofac;

using Macerus.Plugins.Features.Weather;

using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Console
{
    public sealed class ConsoleCommandsBehaviour : MonoBehaviour
    {
        private ProjectXyz.Api.Logging.ILogger _logger;
        private IWeatherAmenity _weatherAmenity;
        private IWeatherManager _weatherManager;
        private IMapFormatter _mapFormatter;
        private IMapProvider _mapProvider;

        private void Start()
        {
            // NOTE: we're violating the stitcher pattern here and *YES* using 
            // the most evil service locator pattern here BUT here me out...
            // This class will probably be mostly for test methods and this is 
            // the easiest shortcut for getting access to all kinds of stuff
            // without making separate classes for stitching etc...
            var container = GameDependencyBehaviour.Container;

            _logger = container.Resolve<ProjectXyz.Api.Logging.ILogger>();
            _weatherAmenity = container.Resolve<IWeatherAmenity>();
            _weatherManager = container.Resolve<IWeatherManager>();
            _mapFormatter = container.Resolve<IMapFormatter>();
            _mapProvider = container.Resolve<IMapProvider>();

            container
                .Resolve<IConsoleCommandRegistrar>()
                .RegisterDiscoverableCommandsFromInstance(this);
        }

        [DiscoverableConsoleCommand("Prints the path between two points.")]
        private void PathBetweenPoints(double startX, double startY, double endX, double endY)
        {
            var path = _mapProvider
                .PathFinder
                .FindPath(
                    new System.Numerics.Vector2((float)startX, (float)startY),
                    new System.Numerics.Vector2((float)endX, (float)endY),
                    new System.Numerics.Vector2(1, 1))
                .ToArray();
            _logger.Info(
                $"Path between ({startX},{startY}) and ({endX},{endY}):\r\n" +
                $"{string.Join("\r\n", path.Select(p => $"\t({p.X},{p.Y})"))}");
        }

        [DiscoverableConsoleCommand("Toggles the map grid lines on or off.")]
        private void MapGridLinesToggle(bool enabled)
        {
            _mapFormatter.ToggleGridLines(enabled);
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
    }
}

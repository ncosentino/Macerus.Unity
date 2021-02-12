using System;
using System.Collections.Generic;
using System.Globalization;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Shared.Framework;

namespace Assets.Scripts.Plugins.Features.Maps.TiledNet
{
    using IMacerusObjectRepository = Macerus.Api.GameObjects.IGameObjectRepositoryFacade;

    public sealed class TiledNetGameObjectRepository : IGameObjectRepository
    {
        private readonly ITiledMapLoader _tiledMapLoader;
        private readonly IMacerusObjectRepository _gameObjectRepository;
        private readonly ILogger _logger;

        public TiledNetGameObjectRepository(
            ITiledMapLoader tiledMapLoader,
            IMacerusObjectRepository gameObjectRepository,
            ILogger logger)
        {
            _tiledMapLoader = tiledMapLoader;
            _gameObjectRepository = gameObjectRepository;
            _logger = logger;
        }

        public IEnumerable<IGameObject> LoadForMap(IIdentifier mapId)
        {
            var tiledMap = _tiledMapLoader.LoadMap(mapId);

            double worldScalingFactor;
            if (tiledMap.Properties.TryGetValue("WorldScalingFactor", out var rawWorldScalingFactor))
            {
                worldScalingFactor = Convert.ToDouble(
                    rawWorldScalingFactor,
                    CultureInfo.InvariantCulture);
            }
            else
            {
                worldScalingFactor = 1;
                _logger.Warn(
                    $"No world scaling factor set for map '{mapId}'. Using the " +
                    $"default of {worldScalingFactor}.");
            }

            var worldScalingFactorX = worldScalingFactor * tiledMap.TileWidth;
            var worldScalingFactorY = worldScalingFactor * tiledMap.TileHeight;
            _logger.Debug(
                $"Using world scaling factor {worldScalingFactor} " +
                $"(x={worldScalingFactorX}, y={worldScalingFactorY}) for map " +
                $"'{mapId}'.");

            foreach (var tiledMapObjectLayer in tiledMap.ObjectLayers)
            {
                foreach (var tiledMapObject in tiledMapObjectLayer.Objects)
                {
                    var properties = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
                    {
                        ["X"] = tiledMapObject.X / worldScalingFactorX,
                        ["Y"] = tiledMapObject.Y / worldScalingFactorY,
                        ["Width"] = tiledMapObject.Width / worldScalingFactorX,
                        ["Height"] = tiledMapObject.Height / worldScalingFactorY,
                        ["$TiledId"] = tiledMapObject.Id,
                        ["$TiledType"] = tiledMapObject.Type,
                    };
                    foreach (var property in tiledMapObject.Properties)
                    {
                        properties[property.Key.TrimStart('$')] = property.Value;
                    }

                    if (!properties.TryGetValue("TypeId", out var typeId))
                    {
                        throw new InvalidOperationException(
                            $"No type ID found on tiled object '{tiledMapObject.Id}' " +
                            $"on map '{mapId}'.");
                    }

                    if (properties.TryGetValue("TemplateId", out var templateId))
                    {
                        var gameObjectFromTemplate = _gameObjectRepository.CreateFromTemplate(
                            new StringIdentifier(typeId.ToString()), // FIXME: assuming string is a bit hacky
                            new StringIdentifier(templateId.ToString()), // FIXME: assuming string is a bit hacky
                            properties);
                        yield return gameObjectFromTemplate;
                        continue;
                    }

                    if (!properties.TryGetValue("UniqueId", out var uniqueId))
                    {
                        throw new InvalidOperationException(
                            $"No unique ID found on tiled object '{tiledMapObject.Id}' " +
                            $"on map '{mapId}'.");
                    }

                    var gameObject = _gameObjectRepository.Load(
                        new StringIdentifier(typeId.ToString()), // FIXME: assuming string is a bit hacky
                        new StringIdentifier(uniqueId.ToString())); // FIXME: assuming string is a bit hacky
                    yield return gameObject;
                }
            }
        }
    }
}
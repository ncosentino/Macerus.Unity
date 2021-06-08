using Assets.Scripts.Plugins.Features.Controls;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.Input;

using Macerus.Plugins.Features.Mapping;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps
{
    public sealed class MapHoverSelectionBehaviour : MonoBehaviour
    {
        public IMapHoverSelectFormatter MapHoverSelectFormatter { get; set; }

        public IPlayerControlConfiguration PlayerControlConfiguration { get; set; }

        public IMouseInput MouseInput { get; set; }

        public IScreenPointToMapCellConverter ScreenPointToMapCellConverter { get; set; }

        private void Start()
        {
            UnityContracts.RequiresNotNull(this, PlayerControlConfiguration, nameof(PlayerControlConfiguration));
            UnityContracts.RequiresNotNull(this, MouseInput, nameof(MouseInput));
            UnityContracts.RequiresNotNull(this, ScreenPointToMapCellConverter, nameof(ScreenPointToMapCellConverter));
            UnityContracts.RequiresNotNull(this, MapHoverSelectFormatter, nameof(MapHoverSelectFormatter));
        }

        private void FixedUpdate()
        {
            if (PlayerControlConfiguration.HoverTileSelection)
            {
                var mapCell = ScreenPointToMapCellConverter.Convert(MouseInput.Position);
                // FIXME: no idea why only x has to be adjusted here... seems
                // SO wrong but it makes it work. otherwise, the y coordinate
                // of the hover shows up right but it's off-by-one for x.
                var correctedForTileCenter = new System.Numerics.Vector2(mapCell.x - 0.5f, mapCell.y);
                MapHoverSelectFormatter.HoverSelectTile(correctedForTileCenter);
            }
        }
    }
}
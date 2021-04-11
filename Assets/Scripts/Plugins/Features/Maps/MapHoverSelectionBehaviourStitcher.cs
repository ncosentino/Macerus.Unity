using Assets.Scripts.Plugins.Features.Controls;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.Input;

namespace Assets.Scripts.Plugins.Features.Maps
{
    public sealed class MapHoverSelectionBehaviourStitcher : IMapHoverSelectionBehaviourStitcher
    {
        private readonly IMapHoverSelectFormatter _mapHoverSelectFormatter;
        private readonly IPlayerControlConfiguration _playerControlConfiguration;
        private readonly IScreenPointToMapCellConverter _screenPointToMapCellConverter;
        private readonly IMouseInput _mouseInput;

        public MapHoverSelectionBehaviourStitcher(
            IMapHoverSelectFormatter mapHoverSelectFormatter,
            IPlayerControlConfiguration playerControlConfiguration,
            IScreenPointToMapCellConverter screenPointToMapCellConverter,
            IMouseInput mouseInput)
        {
            _mapHoverSelectFormatter = mapHoverSelectFormatter;
            _playerControlConfiguration = playerControlConfiguration;
            _screenPointToMapCellConverter = screenPointToMapCellConverter;
            _mouseInput = mouseInput;
        }

        public void Attach(IMapPrefab mapPrefab)
        {           
            var mapHoverSelectionBehaviour = mapPrefab.GameObject.AddComponent<MapHoverSelectionBehaviour>();
            mapHoverSelectionBehaviour.MapHoverSelectFormatter = _mapHoverSelectFormatter;
            mapHoverSelectionBehaviour.PlayerControlConfiguration = _playerControlConfiguration;
            mapHoverSelectionBehaviour.ScreenPointToMapCellConverter = _screenPointToMapCellConverter;
            mapHoverSelectionBehaviour.MouseInput = _mouseInput;
        }
    }
}
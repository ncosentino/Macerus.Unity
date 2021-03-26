using ProjectXyz.Game.Interface.Engine;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore
{
    public sealed class GameEngineUpdateBehaviourStitcher : IGameEngineUpdateBehaviourStitcher
    {
        private readonly IGameEngine _gameEngine;

        public GameEngineUpdateBehaviourStitcher(IGameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        public IReadOnlyGameEngineUpdateBehaviour Attach(GameObject gameObject)
        {
            var gameEngineUpdateBehaviour = gameObject.AddComponent<GameEngineUpdateBehaviour>();
            gameEngineUpdateBehaviour.GameEngine = _gameEngine;
            return gameEngineUpdateBehaviour;
        }
    }
}
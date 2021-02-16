using NexusLabs.Contracts;
using ProjectXyz.Game.Interface.Engine;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore
{
    public sealed class GameEngineUpdateBehaviour :
        MonoBehaviour,
        IGameEngineUpdateBehaviour
    {
        public IGameEngine GameEngine { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                GameEngine,
                $"{nameof(GameEngine)} was not set on '{gameObject}.{this}'.");
        }

        private void Update()
        {
            GameEngine.Update();
        }
    }
}
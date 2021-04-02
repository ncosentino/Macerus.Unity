using System.Threading.Tasks;

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
            UnityContracts.RequiresNotNull(this, GameEngine, nameof(GameEngine));
        }

        //private async void Update()
        //{
        //    await Task.Run(GameEngine.Update);
        //}

        private void Update()
        {
            GameEngine.Update();
        }
    }
}
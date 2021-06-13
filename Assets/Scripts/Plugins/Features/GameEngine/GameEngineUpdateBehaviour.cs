using System.Threading.Tasks;

using NexusLabs.Contracts;

using ProjectXyz.Game.Interface.Engine;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameEngine
{
    public sealed class GameEngineUpdateBehaviour : MonoBehaviour
    {
        public IGameEngine GameEngine { get; set; }

        private void Start()
        {
            this.RequiresNotNull(GameEngine, nameof(GameEngine));
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
using System.Threading.Tasks;

using Assets.Scripts.Unity.Threading;

using NexusLabs.Contracts;

using ProjectXyz.Game.Interface.Engine;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameEngine
{
    public sealed class GameEngineUpdateBehaviour : MonoBehaviour
    {
        private readonly UnityAsyncRunner _runner = new UnityAsyncRunner();

        public IGameEngine GameEngine { get; set; }

        private void Start()
        {
            this.RequiresNotNull(GameEngine, nameof(GameEngine));
        }

        private async Task Update()   
        {
            await _runner.RunAsync(GameEngine.UpdateAsync);
        }
    }
}
using System.Threading;
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
        private readonly CancellationTokenSource _cancellationTokenSource;

        public GameEngineUpdateBehaviour()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public IAsyncGameEngine GameEngine { get; set; }

        private void Start()
        {
            UnityContracts.RequiresNotNull(this, GameEngine, nameof(GameEngine));
            GameEngine.RunAsync(_cancellationTokenSource.Token);
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
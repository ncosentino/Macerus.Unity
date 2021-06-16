using System;

using NexusLabs.Contracts;

using ProjectXyz.Game.Interface.Engine;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameEngine
{
    public sealed class GameEngineUpdateBehaviour : MonoBehaviour
    {
        private bool _stillUpdating;

        public IGameEngine GameEngine { get; set; }

        private void Start()
        {
            this.RequiresNotNull(GameEngine, nameof(GameEngine));
        }

        private async void Update()   
        {
            if (_stillUpdating)
            {
                return;
            }

            _stillUpdating = true;
            await GameEngine
                .UpdateAsync()
                .ContinueWith(prev =>
                {
                    if (prev.IsFaulted)
                    {
                        throw new InvalidOperationException(
                            "Game engine update threw an exception. See inner exception.",
                            prev.Exception);
                    }

                    _stillUpdating = false;
                })
                .ConfigureAwait(false);
        }
    }
}
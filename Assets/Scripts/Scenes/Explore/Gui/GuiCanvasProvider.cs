using System.Linq;
using Assets.Scripts.Scenes.Explore.Gui.Api;
using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui
{
    public sealed class GuiCanvasProvider : IGuiCanvasProvider
    {
        private readonly IGameObjectManager _gameObjectManager;

        public GuiCanvasProvider(IGameObjectManager gameObjectManager)
        {
            _gameObjectManager = gameObjectManager;
        }

        public Canvas GetCanvas() => _gameObjectManager
            .FindAll()
            .First(x => x.name == "Game")
            .GetComponentInChildren<Canvas>();
    }
}

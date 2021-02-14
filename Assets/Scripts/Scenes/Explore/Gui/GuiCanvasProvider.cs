using System.Linq;
using Assets.Scripts.Scenes.Explore.Gui.Api;
using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui
{
    public sealed class GuiCanvasProvider : IGuiCanvasProvider
    {
        private readonly IUnityGameObjectManager _gameObjectManager;

        public GuiCanvasProvider(IUnityGameObjectManager gameObjectManager)
        {
            _gameObjectManager = gameObjectManager;
        }

        public Canvas GetCanvas() => _gameObjectManager
            .FindAll()
            .First(x => x.name == "Game")
            .GetComponentInChildren<Canvas>();
    }
}

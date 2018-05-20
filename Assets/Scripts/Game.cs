using System.IO;
using Assets.Scripts.Autofac;
using Assets.Scripts.Maps;
using Autofac;
using ProjectXyz.Game.Interface.Engine;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class Game : MonoBehaviour
    {   
        private void Start()
        {
            var dependencyContainer = new MacerusContainerBuilder().CreateContainer();

            var gameEngine = dependencyContainer.Resolve<IGameEngine>();
        
            var mapLoader = dependencyContainer.Resolve<IMapLoader>();
            mapLoader.LoadMap(
                GameObject.Find("Map"),
                Path.Combine(Application.dataPath, @"Resources\Mapping\Maps\swamp.tmx"));
        }

        private void Update()
        {

        }
    }
}

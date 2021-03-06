using Assets.Scripts.Behaviours;
using Assets.Scripts.Scenes.MainMenu.Input;

using Autofac;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scenes.MainMenu
{
    public sealed class MainMenuBehaviour : MonoBehaviour
    {
        public void NewGameClick()
        {
            SceneManager.LoadScene("Explore");
        }

        public void ExitClick()
        {
            Debug.Log("Exiting...");
            Application.Quit();
        }

        private void Start()
        {
            var dependencyContainer = GameDependencyBehaviour.Container;
            var guiInputStitcher = dependencyContainer.Resolve<IGuiInputStitcher>();
            guiInputStitcher.Attach(gameObject);
        }
    }
}

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
    }
}

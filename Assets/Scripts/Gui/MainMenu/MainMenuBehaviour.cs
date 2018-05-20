using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class MainMenuBehaviour : MonoBehaviour
{
    public void NewGameClick()
    {
        SceneManager.LoadScene("GameWorld");
    }

    public void ExitClick()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}

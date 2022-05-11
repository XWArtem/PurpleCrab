using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void LoadSceneLevel01()
    {
        SceneManager.LoadScene("Level01");
        // disable pause Canvas if enabled
        if (UIManager.instance.PauseCanvas.enabled) UIManager.instance.PauseCanvas.enabled = false;
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}

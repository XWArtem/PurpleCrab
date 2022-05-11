using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Canvas DeathCanvas = null;
    public Canvas PauseCanvas = null;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance == this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        GameManager.instance.SetUIManager(this);

        GameObject tempObjectDeathCanvas = GameObject.Find("DeathCanvas");
        if (tempObjectDeathCanvas != null)
        {
            DeathCanvas = tempObjectDeathCanvas.GetComponent<Canvas>();
            if (DeathCanvas == null)
            {
                Debug.Log("Couldn't find" + tempObjectDeathCanvas.name);
            }
            DeathCanvas.enabled = false;
        }

        GameObject tempObjectPauseCanvas = GameObject.Find("PauseCanvas");
        if (tempObjectPauseCanvas != null)
        {
            PauseCanvas = tempObjectPauseCanvas.GetComponent<Canvas>();
            if (PauseCanvas == null)
            {
                Debug.Log("Couldn't find" + tempObjectPauseCanvas.name);
            }
            PauseCanvas.enabled = false;
        }
    }
    public void ToggleDeathCanvas()
    {
        DeathCanvas.enabled = !DeathCanvas.enabled;
    }

    public void TogglePauseCanvas()
    {
        PauseCanvas.enabled = !PauseCanvas.enabled;
    }

    public void BackToMenu()
    {
        GameManager.instance.LoadMenu();
    }

    public void RestartLevel()
    {

    }

}

using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Canvas DeathCanvas = null;
    public Canvas PauseCanvas = null;
    public Canvas LevelCompletedCanvas = null;
    public Canvas LevelSelectCanvas = null;
    public LevelSelectCanvas _levelSelectCanvas = null;
    public CrystalsAmountText _crystalsAmountText = null;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance == this) Destroy(gameObject);
        //DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        GameManager.Instance.SetUIManager(this);

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

        GameObject tempObjectLevelCompletedCanvas = GameObject.Find("LevelCompletedCanvas");
        if (tempObjectLevelCompletedCanvas != null)
        {
            LevelCompletedCanvas = tempObjectLevelCompletedCanvas.GetComponent<Canvas>();
            if (LevelCompletedCanvas == null)
            {
                Debug.Log("Couldn't find" + tempObjectLevelCompletedCanvas.name);
            }
            LevelCompletedCanvas.enabled = false;
        }
        GameObject tempObjectLevelSelectCanvas = GameObject.Find("LevelSelectCanvas");
        if (tempObjectLevelSelectCanvas != null)
        {
            LevelSelectCanvas = tempObjectLevelSelectCanvas.GetComponent<Canvas>();
            if (LevelSelectCanvas == null)
            {
                Debug.Log("Couldn't find" + tempObjectLevelSelectCanvas.name);
            }
            LevelSelectCanvas.enabled = false;
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

    public void ToggleLevelCompletedCanvas()
    {
        LevelCompletedCanvas.enabled = !LevelCompletedCanvas.enabled;
    }
    public void ToggleLevelSelectCanvas()
    {
        LevelSelectCanvas.enabled = !LevelSelectCanvas.enabled;
    }
    public void RenewLevelSelectCanvas()
    {
        _levelSelectCanvas.RenewLevelSelectCanvas();
    }
    public void BackToMenu()
    {
        GameManager.Instance.LoadMenu();
    }
    public void NotEnoughCrystals()
    {
        _crystalsAmountText = GameObject.Find("CrystalsAmount").GetComponent<CrystalsAmountText>();
        _crystalsAmountText.RedString();
    }

}

using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Canvas DeathCanvas;
    public Canvas PauseCanvas = null;
    public Canvas LevelCompletedCanvas = null;
    public Canvas LevelSelectCanvas = null;
    public LevelSelectCanvas _levelSelectCanvas = null;
    public CrystalsAmountText _crystalsAmountText = null;
    [SerializeField] private Canvas UIGameBackground;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance == this) Destroy(gameObject);
    }
    private void Start()
    {
        GameManager.Instance.SetUIManager(this);

        DeathCanvas.enabled = false;
        PauseCanvas.enabled = false;
        LevelCompletedCanvas.enabled = false;
        LevelSelectCanvas.enabled = false;
        UIGameBackground.enabled = false;
    }
    public void ToggleUIGameBackground(bool setActive)
    {
        UIGameBackground.enabled = setActive;
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

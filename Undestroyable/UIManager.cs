using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    public static Action<bool> BackgroundFadeAction;

    public static UIManager instance;
    public Canvas DeathCanvas;
    public Canvas PauseCanvas;
    public Canvas LevelCompletedCanvas;
    public Canvas LevelSelectCanvas;
    public LevelSelectCanvas _levelSelectCanvas;
    public CrystalsAmountText _crystalsAmountText;
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
        BackgroundFadeAction?.Invoke(!LevelSelectCanvas.enabled);
    }
    public void RenewLevelSelectCanvas()
    {
        _levelSelectCanvas.RenewLevelSelectCanvas();
    }
    public void NotEnoughCrystals()
    {
        if (_crystalsAmountText == null)
        {
            _crystalsAmountText = FindObjectOfType<CrystalsAmountText>();
        }
        _crystalsAmountText.RedString();
    }
}

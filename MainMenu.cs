using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private StatsPanelText _statsPanelText;
    [SerializeField] private CrystalsAmountText _crystalsAmountText;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private SoundManager _soundManager = null;

    private void Start()
    {
        _settingsPanel?.SetActive(false);
    }
    // used by PLAY button in the main menu
    public void LoadLastOpenLevel()
    {
        StaticActions.LoadLastLevelAction?.Invoke();
    }
    public void SelectLevel()
    {
        GameManager.Instance.SelectLevel();
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
    public void CharacterMoveSpeedUp()
    {
        GameManager.Instance.CharacterMoveSpeedUp();
        _statsPanelText.SetTheString();
        _crystalsAmountText.SetTheString();
    }
    public void CharacterJumpForceUp()
    {
        GameManager.Instance.CharacterJumpForceUp();
        _statsPanelText.SetTheString();
        _crystalsAmountText.SetTheString();
    }
    public void CharacterMoveSpeedDown()
    {
        GameManager.Instance.CharacterMoveSpeedDown();
        _statsPanelText.SetTheString();
        _crystalsAmountText.SetTheString();
    }
    public void CharacterJumpForceDown()
    {
        GameManager.Instance.CharacterJumpForceDown();
        _statsPanelText.SetTheString();
        _crystalsAmountText.SetTheString();
    }
    public void ResetProgress()
    {
        GameManager.Instance.ResetProgress();
        _statsPanelText.SetTheString();
        _crystalsAmountText.SetTheString();
    }
    public void OpenSettingsPanel()
    {
        _settingsPanel.SetActive(true);
    }
    public void CloseSettingsPanel()
    {
        _settingsPanel.SetActive(false);
    }
}
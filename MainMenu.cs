using UnityEngine;

/// <summary>
/// Mainly MainMenu interacts with GameManager and launch its methods. Also it runs some UI features
/// </summary>

public class MainMenu : MonoBehaviour
{
    [SerializeField] private StatsPanelText _statsPanelText;
    [SerializeField] private CrystalsAmountText _crystalsAmountText;
    [SerializeField] private GameObject _settingsPanel = null;
    [SerializeField] private SoundManager _soundManager = null;

    private void Start()
    {
        if (_settingsPanel == null)
        {
            _settingsPanel = GameObject.Find("SettingsPanel");
        }
        //_settingsPanel.enabled = false;
        _settingsPanel.SetActive(false);
    }
    public void LoadCurrentLevel()
    {
        GameManager.instance.LoadCurrentLevel();
    }
    public void SelectLevel()
    {
        GameManager.instance.SelectLevel();
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
    public void CharacterMoveSpeedUp()
    {
        GameManager.instance.CharacterMoveSpeedUp();
        _statsPanelText.SetTheString();
        _crystalsAmountText.SetTheString();
    }
    public void CharacterJumpForceUp()
    {
        GameManager.instance.CharacterJumpForceUp();
        _statsPanelText.SetTheString();
        _crystalsAmountText.SetTheString();
    }
    public void CharacterMoveSpeedDown()
    {
        GameManager.instance.CharacterMoveSpeedDown();
        _statsPanelText.SetTheString();
        _crystalsAmountText.SetTheString();
    }
    public void CharacterJumpForceDown()
    {
        GameManager.instance.CharacterJumpForceDown();
        _statsPanelText.SetTheString();
        _crystalsAmountText.SetTheString();
    }

    public void ResetProgress()
    {
        GameManager.instance.ResetProgress();
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

    public void SetMusicVolume(float musicVolume)
    {
        if (_soundManager == null)
        {
            _soundManager = FindObjectOfType<SoundManager>();
        }
        _soundManager.SetMusicVolume(musicVolume);
    }
}

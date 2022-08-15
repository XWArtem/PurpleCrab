using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private TimeFlow _timeFlow;
    [SerializeField] private LevelCompletedCanvas _levelCompletedCanvas;

    private void Start()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void OnEnable()
    {
        StaticActions.LoadLastLevelAction += LoadLastOpenLevel;
        StaticActions.LoadSceneOnNameAction += LoadSceneOnName;
    }
    private void OnDisable()
    {
        StaticActions.LoadLastLevelAction -= LoadLastOpenLevel;
        StaticActions.LoadSceneOnNameAction -= LoadSceneOnName;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        if (_UIManager.LevelSelectCanvas.enabled == true)
        {
            _UIManager.LevelSelectCanvas.enabled = false;
        }
        _UIManager.ToggleUIGameBackground(false);
        if (Time.timeScale == 0) { Time.timeScale = 1; }
    }
    // used by Restart Button in LevelCompletedCanvas:
    public void RestartLevel()
    {
        FullCheckBeforeLoadLevel(SceneManager.GetActiveScene().buildIndex - 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadLastOpenLevel()
    {
        if (GameManager.Instance.LevelProgress <= 10)
        {
            FullCheckBeforeLoadLevel(GameManager.Instance.LevelProgress + 1);
            SceneManager.LoadScene(GameManager.Instance.LevelProgress + 1);
        }
        else
        {
            FullCheckBeforeLoadLevel(GameManager.Instance.LevelProgress);
            SceneManager.LoadScene(GameManager.Instance.LevelProgress);
        }
    }
    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 11) // reached the last level
        {
            FullCheckBeforeLoadLevel(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            FullCheckBeforeLoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    // loading scene from LevelSelect Canvas:
    public void LoadSceneOnName(int sceneIndex)
    {
        FullCheckBeforeLoadLevel(sceneIndex - 1);
        SceneManager.LoadScene(sceneIndex);
    }
    private void FullCheckBeforeLoadLevel(int levelIndex)
    {
        // disable all Canvases if active
        if (_UIManager.PauseCanvas.enabled) _UIManager.PauseCanvas.enabled = false;
        if (_UIManager.DeathCanvas.enabled) _UIManager.DeathCanvas.enabled = false;
        if (_UIManager.LevelCompletedCanvas.enabled) _UIManager.LevelCompletedCanvas.enabled = false;
        if (_UIManager.LevelSelectCanvas.enabled) _UIManager.LevelSelectCanvas.enabled = false;

        _UIManager.ToggleUIGameBackground(true);

        StaticActions.ToggleInputControllerAction?.Invoke(true);
        StaticActions.TogglePlayerIsDeadAction?.Invoke(false);
        StaticActions.SetCameraActiveAction?.Invoke(true);
        _timeFlow.SetTimeScale(1);
        // update the LevelProgress value in LevelCompletedCanvas
        if (levelIndex >= GameManager.Instance.LevelProgress)
        {
            // The Player is going to pass the level first time! Award him in the end
            _levelCompletedCanvas.UpdateLevelProgress(GameManager.Instance.LevelProgress);
        }
        else
        {
            // this level was already passed
            _levelCompletedCanvas.UpdateLevelProgress(0);
        }
        if (levelIndex == 10)
        {
            _levelCompletedCanvas.DisableNextLevelButton(false);
        }
        else
        {
            _levelCompletedCanvas.DisableNextLevelButton(true);
        }
    }
}

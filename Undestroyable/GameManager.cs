using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player _player = null;
    [SerializeField] private InputController _inputController;
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private CameraFollow _camera;

    private DataArchitecture.StatsRepository statsRepository;
    private DataArchitecture.StatsInteractor statsInteractor;

    [SerializeField] private StatsPanelText _statsPanelText = null;
    [SerializeField] private LevelCompletedCanvas _levelCompletedCanvas = null;

    public float CharacterMoveSpeed;
    public float CharacterJumpForce;
    public int Crystals;
    public int LevelProgress;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // instantly load the Undestroyable scene, find everything we need and go to the menu
    private void Start()
    {
        statsRepository = new DataArchitecture.StatsRepository();
        statsRepository.Initialize();
        statsInteractor = new DataArchitecture.StatsInteractor(statsRepository);

        CharacterMoveSpeed = statsInteractor.characterMoveSpeed;
        CharacterJumpForce = statsInteractor.characterJumpForce;
        Crystals = statsInteractor.crystals;
        LevelProgress = statsInteractor.levelProgress;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // set all objects to the GameManager
    public void SetInputController(InputController c) { _inputController = c; }
    public void SetPlayer(Player p) { _player = p; }
    public void SetUIManager(UIManager ui) { _UIManager = ui; }
    public void SetCamera(CameraFollow cf) { _camera = cf; }

    private void Update()
    { 
        if (_inputController.LoadNextScene()) LoadNextLevel();
        if (_inputController.PauseGameInput()) PauseGame();
        if (_inputController.AddCrystalsInput()) AddCrystalsTest();
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        _player.CharacterIsDead = true;
        UIManager.instance.ToggleDeathCanvas();
        // disable input controller and camera following
        _inputController.ControlEnabled = false;
        _camera.cameraIsActive = false;
    }

    public void LevelCompleted(int levelIndex)
    {
        Debug.Log("Level completed!");
        Debug.Log("Scene Index is :" + levelIndex);
        // disable input controller and camera following
        _inputController.ControlEnabled = false;
        _camera.cameraIsActive = false;
        if (Time.timeScale != 0f) Time.timeScale = 0f;
        _UIManager.ToggleLevelCompletedCanvas();

        // add LevelProgress if this LEVEL is completed the FIRST TIME
        if (levelIndex > LevelProgress)
        {
            LevelProgress++;
            statsInteractor.LevelProgressUp();
            AddCrystals(levelIndex-1);
            Debug.Log("Current LevelProgress is:" + LevelProgress);
            _UIManager.RenewLevelSelectCanvas();
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        if (_UIManager.LevelSelectCanvas.enabled == true) _UIManager.LevelSelectCanvas.enabled = false;
        if (Time.timeScale == 0) Time.timeScale = 1;
        //_soundManager.PlayMusic01();
    }
    public void PauseGame()
    {
        _UIManager.TogglePauseCanvas();
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        else Time.timeScale = 0f;
    }

    // used by Restart Button in LevelCompletedCanvas :
    public void RestartLevel()
    {
        // full check first
        FullCheckBeforeLoadLevel(SceneManager.GetActiveScene().buildIndex - 1);
        // reload the current level then
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void LoadLastOpenLevel()
    {
        // full check first
        if (LevelProgress <= 10)
        {
            FullCheckBeforeLoadLevel(LevelProgress + 1);
            SceneManager.LoadScene(LevelProgress + 1);
        }
        else
        {
            FullCheckBeforeLoadLevel(LevelProgress);
            SceneManager.LoadScene(LevelProgress);
        }
    }

    public void LoadNextLevel()
    {
        if (LevelProgress <= 10)
        {
            // full check first
            FullCheckBeforeLoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
            // load next then
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            FullCheckBeforeLoadLevel(SceneManager.GetActiveScene().buildIndex);
            // load next then
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // loading scene from LevelSelect Canvas:
    public void LoadSceneOnName(int sceneIndex)
    {
        // full check first
        FullCheckBeforeLoadLevel(sceneIndex-1);
        // load the scene by name then
        SceneManager.LoadScene(sceneIndex);
    }

    public void SelectLevel()
    {
        _UIManager.ToggleLevelSelectCanvas();
    }

    private void FullCheckBeforeLoadLevel(int levelIndex)
    {
        // disable all Canvases if active
        if (_UIManager.PauseCanvas.enabled) _UIManager.PauseCanvas.enabled = false;
        if (_UIManager.DeathCanvas.enabled) _UIManager.DeathCanvas.enabled = false;
        if (_UIManager.LevelCompletedCanvas.enabled) _UIManager.LevelCompletedCanvas.enabled = false;
        if (_UIManager.LevelSelectCanvas.enabled) _UIManager.LevelSelectCanvas.enabled = false;

        // reset the values about Character death and input controller
        _inputController.ControlEnabled = true;
        if (_player != null) _player.CharacterIsDead = false;

        // enable camera
        if (_camera != null) _camera.cameraIsActive = true;

        // start Timer
        if (Time.timeScale == 0f) Time.timeScale = 1f;

        // update the LevelProgress value in LevelCompletedCanvas
        _levelCompletedCanvas = GameObject.FindObjectOfType<LevelCompletedCanvas>();

        // I need to check if the level is new to a Player and reward him crystals.
        // If not, we put 0 crystals in LevelCompletedCanvas
        // make some Log outputs for easier understanding what's going on:
        Debug.Log("Level index is: " + levelIndex);
        Debug.Log("LevelProgress is: " + LevelProgress);

        if (levelIndex >= LevelProgress)
        {
            // The Player passed the level first time! Award him
            _levelCompletedCanvas.UpdateLevelProgress(LevelProgress);
        }
        else
        {
            // this level was already passed
            _levelCompletedCanvas.UpdateLevelProgress(0);
        }
    }

    private void AddCrystalsTest()
    { 
        this.statsInteractor.AddCrystals(this,160);
        Crystals = this.statsInteractor.crystals;
        // make some Log outputs. If not needed, just comment them
        Debug.Log($"Currently I have, {statsInteractor.crystals} crystals in Interactor");
        Debug.Log($"Currently I have, {Crystals} crystals in GM");
        Debug.Log($"My level progress is " + LevelProgress);
    }
    // TEST FUNCTION FOR ADDING 1 CRYSTAL -- START//
    public void AddOneCrystal()
    {
        statsInteractor.AddCrystals(this, 1);
        Crystals = statsInteractor.crystals;
    }
    // TEST FUNCTION FOR ADDING 1 CRYSTAL -- FINISH//

    public void AddCrystals(int value)
    {
        statsInteractor.AddCrystals(this, value);
        Crystals = statsInteractor.crystals;
    }

    public void CharacterMoveSpeedUp()
    {
        if (statsInteractor.IsEnoughCrystals(10))
        {
            Debug.Log("I have " + statsInteractor.crystals + 
                "crystals in Interactor before Spent 10 of them");

            statsInteractor.SpendCrystals(this, 10);
            Debug.Log("I have " + statsInteractor.crystals + 
                "crystals in Interactor after Spent 10 of them");

            statsInteractor.CharacterMoveSpeedUp(this, 1);
            Debug.Log("Character Move Speed is" + statsInteractor.characterMoveSpeed);

            // Refresh the intern values to use it in StatsPanelText
            CharacterMoveSpeed = statsInteractor.characterMoveSpeed;
            Crystals = statsInteractor.crystals;
        }
        else _UIManager.NotEnoughCrystals();
    }

    public void CharacterJumpForceUp()
    {
        if (statsInteractor.IsEnoughCrystals(10))
        {
            Debug.Log("I have " + statsInteractor.crystals + 
                "crystals in Interactor before Spent 10 of them");
            statsInteractor.SpendCrystals(this, 10);
            Debug.Log("I have " + statsInteractor.crystals + 
                "crystals in Interactor after Spent 10 of them");

            statsInteractor.CharacterJumpForceUp(this, 1);
            Debug.Log("Character Jump Foce is" + statsInteractor.characterJumpForce);

            // Refresh the intern values to use it in StatsPanelText
            CharacterJumpForce = statsInteractor.characterJumpForce;
            Crystals = statsInteractor.crystals;
        }
        else _UIManager.NotEnoughCrystals();
    }

    public void CharacterMoveSpeedDown()
    {
        if (statsInteractor.IsEnoughCharacterMoveSpeed(6))
        {
            Debug.Log("I have " + statsInteractor.crystals + 
                "crystals in Interactor before Spent 10 of them");

            statsInteractor.AddCrystals(this, 10);
            Debug.Log("I have " + statsInteractor.crystals + 
                "crystals in Interactor after Spent 10 of them");

            statsInteractor.CharacterMoveSpeedUp(this, -1);
            Debug.Log("Character Move Speed is" + statsInteractor.characterMoveSpeed);

            // Refresh the intern values to use it in StatsPanelText
            CharacterMoveSpeed = statsInteractor.characterMoveSpeed;
            Crystals = statsInteractor.crystals;
        }
        else
        {
            _statsPanelText = FindObjectOfType<StatsPanelText>();
            _statsPanelText.InformAboutMoveSpeedLimit(6);
        }
    }

    public void CharacterJumpForceDown()
    {
        if (statsInteractor.IsEnoughCharacterJumpForce(6))
        {
            Debug.Log("I have " + statsInteractor.crystals + 
                "crystals in Interactor before Spent 10 of them");
            statsInteractor.AddCrystals(this, 10);
            Debug.Log("I have " + statsInteractor.crystals + 
                "crystals in Interactor after Spent 10 of them");
            statsInteractor.CharacterJumpForceUp(this, -1);
            Debug.Log("Character Jump Foce is" + statsInteractor.characterJumpForce);

            // Refresh the intern values to use it in StatsPanelText
            CharacterJumpForce = statsInteractor.characterJumpForce;
            Crystals = statsInteractor.crystals;
        }
        else
        {
            _statsPanelText = FindObjectOfType<StatsPanelText>();
            _statsPanelText.InformAboutJumpForceLimit(6);
        }
    }
    public void ResetProgress()
    {
        statsInteractor.ResetAllStats();
        Crystals = statsInteractor.crystals;
        CharacterMoveSpeed = statsInteractor.characterMoveSpeed;
        CharacterJumpForce = statsInteractor.characterJumpForce;
        LevelProgress = statsInteractor.levelProgress;
        _UIManager.RenewLevelSelectCanvas();
    }
}
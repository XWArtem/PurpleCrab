using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// That's the main script that controls core game logic
/// </summary>

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] private Player _player = null;
    [SerializeField] private InputController _inputController;
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private CameraFollow _camera;
    [SerializeField] private SoundManager _soundManager = null;

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
        if (instance == null) instance = this;
        else if (instance == this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // instantly load the Undestroyable scene, find everything we need and go to the menu
    private void Start()
    {
        this.statsRepository = new DataArchitecture.StatsRepository();
        this.statsRepository.Initialize();
        this.statsInteractor = new DataArchitecture.StatsInteractor(this.statsRepository);

        CharacterMoveSpeed = this.statsInteractor.characterMoveSpeed;
        CharacterJumpForce = this.statsInteractor.characterJumpForce;
        Crystals = this.statsInteractor.crystals;
        LevelProgress = this.statsInteractor.levelProgress;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Time Scale is" + Time.timeScale);
    }

    // set all possible objects to the GameManager
    public void SetInputController(InputController c) { _inputController = c; }
    public void SetPlayer(Player p) { _player = p; }
    public void SetUIManager(UIManager ui) { _UIManager = ui; }
    public void SetCamera(CameraFollow cf) { _camera = cf; }
    public void SetAudioManager(SoundManager sm) { _soundManager = sm; }

    private void Update()
    { 
        if (_inputController.LoadNextScene()) LoadNextLevel();
        if (_inputController.PauseGameInput()) PauseGame();
        if (_inputController.AddCrystalsInput()) AddCrystalsTest();
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        //gameObject.GetComponent<Player>().CharacterIsDead = true;
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
            this.statsInteractor.LevelProgressUp();
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
    public void RestartLevel()
    {
        // full check first
        FullCheckBeforeLoadLevel(SceneManager.GetActiveScene().buildIndex - 1);
        // reload the current level then
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// ===== THIS SCRIPT WILL BE DELETED LATER (START)=====////
    public void LoadCurrentLevel()
    {
        // full check first
        if (LevelProgress <= 10)
        {
            FullCheckBeforeLoadLevel(LevelProgress + 1);
            // load level 01 then
            SceneManager.LoadScene(LevelProgress + 1);
        }
        else
        {
            FullCheckBeforeLoadLevel(LevelProgress);
            // load level 01 then
            SceneManager.LoadScene(LevelProgress);
        }
    }
    /// ===== THIS SCRIPT WILL BE DELETED LATER (FINISH) =====////
    public void LoadNextLevel()
    {
        if (LevelProgress <= 10)
        {
            // full check first
            FullCheckBeforeLoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
            // load next then
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //_UIManager.RenewLevelSelectCanvas();
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
        Debug.Log("My GM has levelProgress now: " + LevelProgress);

        // I need to check if the level is new to a Player and reward him crystals. If not, I should put 0 crystals in LevelCompletedCanvas
        Debug.Log("Level index is: " + levelIndex);
        Debug.Log("LevelProgress is: " + LevelProgress);
        if (levelIndex >= LevelProgress)
        {
            _levelCompletedCanvas.UpdateLevelProgress(LevelProgress);
        }
        else
        {
            _levelCompletedCanvas.UpdateLevelProgress(0);
        }
    }

    private void AddCrystalsTest()
    {
        Debug.Log("Some crystals added");
        this.statsInteractor.AddCrystals(this,160);
        Crystals = this.statsInteractor.crystals;
        Debug.Log($"Currently I have, {this.statsInteractor.crystals} crystals in Interactor");
        Debug.Log($"Currently I have, {Crystals} crystals in GM");
        Debug.Log($"My level progress is " + LevelProgress);
    }
    // DELETE IT LATER -- START//
    public void AddOneCrystal()
    {
        this.statsInteractor.AddCrystals(this, 1);
        Crystals = this.statsInteractor.crystals;
    }
    // DELETE IT LATER -- FINISH//

    public void AddCrystals(int value)
    {
        this.statsInteractor.AddCrystals(this, value);
        Crystals = this.statsInteractor.crystals;
    }
    public void CharacterMoveSpeedUp()
    {
        if (this.statsInteractor.IsEnoughCrystals(10))
        {
            Debug.Log("I have " + this.statsInteractor.crystals + "crystals in Interactor before Spent 10 of them");
            this.statsInteractor.SpendCrystals(this, 10);
            Debug.Log("I have " + this.statsInteractor.crystals + "crystals in Interactor after Spent 10 of them");
            this.statsInteractor.CharacterMoveSpeedUp(this, 1);
            Debug.Log("Character Move Speed is" + this.statsInteractor.characterMoveSpeed);

            // Renew the intern values to use it in StatsPanelText
            CharacterMoveSpeed = this.statsInteractor.characterMoveSpeed;
            Crystals = this.statsInteractor.crystals;
        }
        else _UIManager.NotEnoughCrystals();
    }

    public void CharacterJumpForceUp()
    {
        if (this.statsInteractor.IsEnoughCrystals(10))
        {
            Debug.Log("I have " + this.statsInteractor.crystals + "crystals in Interactor before Spent 10 of them");
            this.statsInteractor.SpendCrystals(this, 10);
            Debug.Log("I have " + this.statsInteractor.crystals + "crystals in Interactor after Spent 10 of them");
            this.statsInteractor.CharacterJumpForceUp(this, 1);
            Debug.Log("Character Jump Foce is" + this.statsInteractor.characterJumpForce);

            // Renew the intern values to use it in StatsPanelText
            CharacterJumpForce = this.statsInteractor.characterJumpForce;
            Crystals = this.statsInteractor.crystals;
        }
        else _UIManager.NotEnoughCrystals();
    }

    public void CharacterMoveSpeedDown()
    {
        if (this.statsInteractor.IsEnoughCharacterMoveSpeed(6))
        {
            Debug.Log("I have " + this.statsInteractor.crystals + "crystals in Interactor before Spent 10 of them");
            this.statsInteractor.AddCrystals(this, 10);
            Debug.Log("I have " + this.statsInteractor.crystals + "crystals in Interactor after Spent 10 of them");
            this.statsInteractor.CharacterMoveSpeedUp(this, -1);
            Debug.Log("Character Move Speed is" + this.statsInteractor.characterMoveSpeed);

            // Renew the intern values to use it in StatsPanelText
            CharacterMoveSpeed = this.statsInteractor.characterMoveSpeed;
            Crystals = this.statsInteractor.crystals;
        }
        else
        {
            _statsPanelText = FindObjectOfType<StatsPanelText>();
            _statsPanelText.InformAboutMoveSpeedLimit(6);
        }
    }

    public void CharacterJumpForceDown()
    {
        if (this.statsInteractor.IsEnoughCharacterJumpForce(6))
        {
            Debug.Log("I have " + this.statsInteractor.crystals + "crystals in Interactor before Spent 10 of them");
            this.statsInteractor.AddCrystals(this, 10);
            Debug.Log("I have " + this.statsInteractor.crystals + "crystals in Interactor after Spent 10 of them");
            this.statsInteractor.CharacterJumpForceUp(this, -1);
            Debug.Log("Character Jump Foce is" + this.statsInteractor.characterJumpForce);

            // Renew the intern values to use it in StatsPanelText
            CharacterJumpForce = this.statsInteractor.characterJumpForce;
            Crystals = this.statsInteractor.crystals;
        }
        else
        {
            _statsPanelText = FindObjectOfType<StatsPanelText>();
            _statsPanelText.InformAboutJumpForceLimit(6);
        }
    }
    public void ResetProgress()
    {
        this.statsInteractor.ResetAllStats();
        Crystals = this.statsInteractor.crystals;
        CharacterMoveSpeed = this.statsInteractor.characterMoveSpeed;
        CharacterJumpForce = this.statsInteractor.characterJumpForce;
        LevelProgress = this.statsInteractor.levelProgress;
        _UIManager.RenewLevelSelectCanvas();
    }
}

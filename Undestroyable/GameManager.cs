using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player _player;
    [SerializeField] private InputController _inputController;
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private MainHeroSpawner _mainHeroSpawner;

    private DataArchitecture.StatsRepository _statsRepository;
    private DataArchitecture.StatsInteractor _statsInteractor;

    [SerializeField] private StatsPanelText _statsPanelText = null;
    [SerializeField] private LevelCompletedCanvas _levelCompletedCanvas = null;
    private CrystalsAmountText _crystalsAmountText = null;

    public float CharacterMoveSpeed;
    public float CharacterJumpForce;
    private int _crystals;
    public int Crystals 
    {
        get 
        {
            return _crystals;
        }
        set 
        {
            _crystals = value;
            if (_crystalsAmountText != null)
            {
                _crystalsAmountText.Crystals = value;
            }
        }
    }
    public int LevelProgress;

    private void Awake()
    {
        
        if (Instance == null) { Instance = this; }
        else Destroy(gameObject);
    }
    // instantly load the Undestroyable scene, find everything we need and go to the menu
    private void Start()
    {
        _statsRepository = new DataArchitecture.StatsRepository();
        _statsRepository.Initialize();
        _statsInteractor = new DataArchitecture.StatsInteractor(_statsRepository);

        CharacterMoveSpeed = _statsInteractor.CharacterMoveSpeed;
        CharacterJumpForce = _statsInteractor.CharacterJumpForce;
        Crystals = _statsInteractor.Crystals;
        LevelProgress = _statsInteractor.LevelProgress;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // set all objects to the GameManager
    public void SetInputController(InputController inputController) { _inputController = inputController; }
    //public void SetPlayer(Player player) { _player = player; } ÓÁÐÀÒÜ
    public void SetUIManager(UIManager UImanager) { _UIManager = UImanager; }
    public void SetCamera(CameraFollow cameraFollow) { _cameraFollow = cameraFollow; }
    public void SetCrystalsAmountText(CrystalsAmountText crystalsAmountText) 
    { 
        _crystalsAmountText = crystalsAmountText;
        crystalsAmountText.Crystals = _crystals;
    }
    public void SetStatsPanelText(StatsPanelText statsPanelText) { _statsPanelText = statsPanelText; }
    private void Update()
    {
        if (_inputController.LoadNextScene()) { LoadNextLevel(); }
        if (_inputController.PauseGameInput()) { PauseGame(); }
        if (_inputController.AddCrystalsInput()) { AddCrystalsTest(); }
    }
    public void GameOver()
    {
        Debug.Log("Game Over!");
        _player.CharacterIsDead = true;
        UIManager.instance.ToggleDeathCanvas();
        // disable input controller and camera following
        _inputController.ControlEnabled = false;
        _cameraFollow.CameraIsActive = false;
    }
    public void LevelCompleted(int levelIndex)
    {
        Debug.Log("Level completed!");
        Debug.Log("Scene Index is :" + levelIndex);
        // disable input controller and camera following
        _inputController.ControlEnabled = false;
        _cameraFollow.CameraIsActive = false;
        if (Time.timeScale != 0f) { Time.timeScale = 0f; }
        _UIManager.ToggleLevelCompletedCanvas();

        // add LevelProgress if this LEVEL is completed the FIRST TIME
        if (levelIndex > LevelProgress)
        {
            LevelProgress++;
            _statsInteractor.LevelProgressUp();
            AddCrystals(levelIndex-1);
            Debug.Log("Current LevelProgress is:" + LevelProgress);
            _UIManager.RenewLevelSelectCanvas();
        }
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
    public void PauseGame()
    {
        _UIManager.TogglePauseCanvas();
        if (Time.timeScale == 0f) { Time.timeScale = 1f; }
        else Time.timeScale = 0f;
    }
    // used by Restart Button in LevelCompletedCanvas :
    public void RestartLevel()
    {
        FullCheckBeforeLoadLevel(SceneManager.GetActiveScene().buildIndex - 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadLastOpenLevel()
    {
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
            FullCheckBeforeLoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            FullCheckBeforeLoadLevel(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    // loading scene from LevelSelect Canvas:
    public void LoadSceneOnName(int sceneIndex)
    {
        FullCheckBeforeLoadLevel(sceneIndex-1);
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

        
        // enable in-game Canvas background
        _UIManager.ToggleUIGameBackground(true);

        // reset the values about Character death and input controller
        _inputController.ControlEnabled = true;
        if (_player != null) 
        {
            _player.CharacterIsDead = false; 
        }
        // enable camera
        if (_cameraFollow != null)
        {
            _cameraFollow.CameraIsActive = true;
        }
        // start Timer
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        // update the LevelProgress value in LevelCompletedCanvas
        _levelCompletedCanvas = GameObject.FindObjectOfType<LevelCompletedCanvas>();

        // I need to check if the level is new to a Player and reward him crystals.
        // If not, we put 0 crystals in LevelCompletedCanvas

        if (levelIndex >= LevelProgress)
        {
            // The Player passed the level first time! Award him in the end
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
        _statsInteractor.AddCrystals(this,160);
        Crystals = _statsInteractor.Crystals;
    }
    // TEST FUNCTION FOR ADDING 1 CRYSTAL -- START//
    public void AddOneCrystal()
    {
        _statsInteractor.AddCrystals(this, 1);
        Crystals = _statsInteractor.Crystals;
    }
    // TEST FUNCTION FOR ADDING 1 CRYSTAL -- FINISH//
    public void AddCrystals(int value)
    {
        _statsInteractor.AddCrystals(this, value);
        Crystals = _statsInteractor.Crystals;
    }
    public void CharacterMoveSpeedUp()
    {
        if (CharacterMoveSpeed < 20.0f)
        {
            if (_statsInteractor.IsEnoughCrystals(10))
            {
                _statsInteractor.SpendCrystals(this, 10);
                _statsInteractor.CharacterMoveSpeedUp(this, 1.0f);

                // Refresh the intern values to use it in StatsPanelText
                CharacterMoveSpeed = _statsInteractor.CharacterMoveSpeed;
                Crystals = _statsInteractor.Crystals;
            }
            else _UIManager.NotEnoughCrystals();
        }
        else
        {
            //_statsPanelText = FindObjectOfType<StatsPanelText>();
            _statsPanelText.InformAboutMoveSpeedLimit(this, 20);
        }
    }
    public void CharacterJumpForceUp()
    {
        if (CharacterJumpForce < 20.0f)
        {
            if (_statsInteractor.IsEnoughCrystals(10))
            {
                _statsInteractor.SpendCrystals(this, 10);
                _statsInteractor.CharacterJumpForceUp(this, 1);

                // Refresh the intern values to use it in StatsPanelText
                CharacterJumpForce = _statsInteractor.CharacterJumpForce;
                Crystals = _statsInteractor.Crystals;
            }
            else _UIManager.NotEnoughCrystals();
        }
        else
        {
            _statsPanelText = FindObjectOfType<StatsPanelText>();
            _statsPanelText.InformAboutJumpForceLimit(this, 20);
        }
    }
    public void CharacterMoveSpeedDown()
    {
        if (_statsInteractor.IsEnoughCharacterMoveSpeed(6))
        {
            _statsInteractor.AddCrystals(this, 10);
            _statsInteractor.CharacterMoveSpeedUp(this, -1.0f);
            // Refresh the intern values to use it in StatsPanelText
            CharacterMoveSpeed = _statsInteractor.CharacterMoveSpeed;
            Crystals = _statsInteractor.Crystals;
        }
        else
        {
            _statsPanelText = FindObjectOfType<StatsPanelText>();
            _statsPanelText.InformAboutMoveSpeedLimit(this, 6);
        }
    }
    public void CharacterJumpForceDown()
    {
        if (_statsInteractor.IsEnoughCharacterJumpForce(6))
        {
            Debug.Log("I have " + _statsInteractor.Crystals + 
                "crystals in Interactor before Spent 10 of them");
            _statsInteractor.AddCrystals(this, 10);
            Debug.Log("I have " + _statsInteractor.Crystals + 
                "crystals in Interactor after Spent 10 of them");
            _statsInteractor.CharacterJumpForceUp(this, -1);
            Debug.Log("Character Jump Foce is" + _statsInteractor.CharacterJumpForce);

            // Refresh the intern values to use it in StatsPanelText
            CharacterJumpForce = _statsInteractor.CharacterJumpForce;
            Crystals = _statsInteractor.Crystals;
        }
        else
        {
            _statsPanelText = FindObjectOfType<StatsPanelText>();
            _statsPanelText.InformAboutJumpForceLimit(this, 6);
        }
    }
    public void ResetProgress()
    {
        _statsInteractor.ResetAllStats();
        Crystals = _statsInteractor.Crystals;
        CharacterMoveSpeed = _statsInteractor.CharacterMoveSpeed;
        CharacterJumpForce = _statsInteractor.CharacterJumpForce;
        LevelProgress = _statsInteractor.LevelProgress;
        _UIManager.RenewLevelSelectCanvas();
    }
}
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player _player;
    [SerializeField] private InputController _inputController;
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private MainHeroSpawner _mainHeroSpawner;
    [SerializeField] private TimeFlow _timeFlow;

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

        _statsRepository = new DataArchitecture.StatsRepository();
        _statsRepository.Initialize();
        _statsInteractor = new DataArchitecture.StatsInteractor(_statsRepository);

        CharacterMoveSpeed = _statsInteractor.CharacterMoveSpeed;
        CharacterJumpForce = _statsInteractor.CharacterJumpForce;
        Crystals = _statsInteractor.Crystals;
        LevelProgress = _statsInteractor.LevelProgress;
    }

    public void SetInputController(InputController inputController) { _inputController = inputController; }
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
        // ===== DEV MODE ONLY:
        //if (_inputController.LoadNextScene()) { LoadNextLevel(); }
        //if (_inputController.AddCrystalsInputTest()) { AddCrystalsTest(); }

        if (_inputController.PauseGameInput()) { PauseGame(); }
    }
    // ===== DEV MODE ONLY:
    //private void AddCrystalsTest()
    //{
    //    _statsInteractor.AddCrystals(this, 160);
    //    Crystals = _statsInteractor.Crystals;
    //}
    //public void AddOneCrystal()
    //{
    //    _statsInteractor.AddCrystals(this, 1);
    //    Crystals = _statsInteractor.Crystals;
    //}
    public void GameOver()
    {
        StaticActions.TogglePlayerIsDeadAction?.Invoke(true);
        UIManager.instance.ToggleDeathCanvas();
        // disable input controller and camera following
        StaticActions.ToggleInputControllerAction?.Invoke(false);
        StaticActions.SetCameraActiveAction?.Invoke(false);
    }
    public void LevelCompleted(int levelIndex)
    {
        StaticActions.ToggleInputControllerAction?.Invoke(false);
        StaticActions.SetCameraActiveAction?.Invoke(false);
        _timeFlow.SetTimeScale(0);
        _UIManager.ToggleLevelCompletedCanvas();

        // add LevelProgress if this LEVEL is completed the FIRST TIME
        if (levelIndex > LevelProgress)
        {
            LevelProgress++;
            _statsInteractor.LevelProgressUp();
            AddCrystals(levelIndex - 1);
            Debug.Log("Current LevelProgress is:" + LevelProgress);
            _UIManager.RenewLevelSelectCanvas();
        }
    }
    public void PauseGame()
    {
        _UIManager.TogglePauseCanvas();
        if (Time.timeScale == 0f) { Time.timeScale = 1f; }
        else Time.timeScale = 0f;
    }
    public void SelectLevel()
    {
        _UIManager.ToggleLevelSelectCanvas();
    }
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
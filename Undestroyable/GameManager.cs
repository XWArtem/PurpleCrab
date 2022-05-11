using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] private Player _player = null;
    public InputController _inputController;
    [SerializeField] private UIManager _UIManager;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance == this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    // skip the Undestroyable scene to the menu
    private void Start()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SetInputController(InputController c)
    {
        _inputController = c;
    }
    public void SetPlayer(Player p)
    {
        _player = p;
    }
    public void SetUIManager(UIManager ui)
    {
        _UIManager = ui;
    }

    private void Update()
    { 
        if (_inputController.LoadNextScene()) { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1 );
        }
        if (_inputController.PauseGameInput())
        {
            PauseGame();
            Time.timeScale = 0f;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        //gameObject.GetComponent<Player>().CharacterIsDead = true;
        _player.CharacterIsDead = true;
        UIManager.instance.ToggleDeathCanvas();
        _inputController.ControlEnabled = false;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void PauseGame()
    {
        _UIManager.TogglePauseCanvas();
    }
}

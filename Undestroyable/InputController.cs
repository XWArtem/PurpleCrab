using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController instance = null;

    public bool ControlEnabled = true;
    private void Start()
    {
        GameManager.instance.SetInputController(this);
        if (instance == null) instance = this;
        else if (instance == this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public bool LoadNextScene()
    {
        if (ControlEnabled)
            return Input.GetKey(KeyCode.F1);
        else return false;
    }
    public float MoveInput()
    {
        if (ControlEnabled)
            return Input.GetAxis("Horizontal");
        else return 0;
    }
    public float JumpInput()
    {
        if (ControlEnabled && Input.GetKey(KeyCode.Space))
            return 1f;
        else return 0f;
    }

    public bool PauseGameInput()
    {
            return Input.GetKeyDown(KeyCode.Escape);
    }
}

using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool ControlEnabled = true;
    private void Start()
    {
        GameManager.Instance.SetInputController(this);
    }
    public bool LoadNextScene()
    {
        if (ControlEnabled)
        {
            return Input.GetKey(KeyCode.F1);
        }
        else return false;
    }
    public float MoveInput()
    {
        if (ControlEnabled)
        {
            return Input.GetAxis("Horizontal");
        }
        else return 0;
    }
    public float JumpInput()
    {
        if (ControlEnabled && Input.GetKey(KeyCode.Space))
        {
            return 1f;
        }
        else return 0f;
    }
    public bool PauseGameInput()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
    public bool AddCrystalsInput()
    {
        return Input.GetKeyDown(KeyCode.F2);
    }
}

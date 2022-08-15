using UnityEngine;

public class InputController : MonoBehaviour
{
    private bool ControlEnabled = true;
    private void Start()
    {
        GameManager.Instance.SetInputController(this);
    }
    private void OnEnable()
    {
        StaticActions.ToggleInputControllerAction += ToggleInputController;
    }
    private void OnDisable()
    {
        StaticActions.ToggleInputControllerAction -= ToggleInputController;
    }
    // ===== DEV MODE. START

    //public bool LoadNextScene()
    //{
    //    if (ControlEnabled)
    //    {
    //        return Input.GetKey(KeyCode.F1);
    //    }
    //    else return false;
    //}
    //public bool AddCrystalsInputTest()
    //{
    //    return Input.GetKeyDown(KeyCode.F2);
    //}
    // ===== DEV MODE. END
    private void ToggleInputController(bool setEnabled)
    {
        ControlEnabled = setEnabled;
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
}

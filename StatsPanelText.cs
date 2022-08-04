using UnityEngine;
using TMPro;

public class StatsPanelText : MonoBehaviour
{
    public TextMeshProUGUI StatsText;
    [SerializeField] private TextMeshProUGUI _moveSpeedLimit;
    [SerializeField] private TextMeshProUGUI _jumpForceLimit;

    private void Awake()
    {
        GameManager.Instance.SetStatsPanelText(this);
    }
    private void Start()
    {
        SetTheString();
        DisableInformationOfSpeedLimit();
        DisableInformationOfJumpForce();
    }

    public void SetTheString()
    {
        StatsText.text = "Speed:  " + GameManager.Instance.CharacterMoveSpeed + "/20"
            + "<br>Jump force:  " + GameManager.Instance.CharacterJumpForce + "/20";
            //+ "<br>Health:"
            //+ "<br>Weapon:"
            //+ "<br>Special ability:";
    }
    private void DisableInformationOfSpeedLimit()
    {
        _moveSpeedLimit.enabled = false;
    }
    private void DisableInformationOfJumpForce()
    {
        _jumpForceLimit.enabled = false;
    }
    public void InformAboutMoveSpeedLimit(object sender, int moveSpeedLimit)
    {
        if (moveSpeedLimit != 20 && moveSpeedLimit != 6)
        {
            Debug.Log($"{sender} passed invalid value to InformAboutMoveSpeedLimit");
        }
        switch (moveSpeedLimit) 
        {
            case 20:
                _moveSpeedLimit.text = CONSTSTRINGS.MoveSpeedLimitMax;
                break;
            case 6:
                _moveSpeedLimit.text = CONSTSTRINGS.MoveSpeedLimitMin;
                break;
        }
        _moveSpeedLimit.enabled = true;
        Invoke(nameof(DisableInformationOfSpeedLimit), 2f);
    }
    public void InformAboutJumpForceLimit(object sender, int jumpForceLimit)
    {
        if (jumpForceLimit != 20 && jumpForceLimit != 6)
        {
            Debug.Log($"{sender} passed invalid value to InformAboutJumpForceLimit");
        }
        switch (jumpForceLimit)
        {
            case 20:
                _jumpForceLimit.text = CONSTSTRINGS.JumpForceLimitMax;
                break;
            case 6:
                _jumpForceLimit.text = CONSTSTRINGS.JumpForceLimitMin;
                break;
        }
        _jumpForceLimit.enabled = true;
        Invoke(nameof(DisableInformationOfJumpForce), 2f);
    }
}

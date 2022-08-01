using UnityEngine;
using TMPro;

public class StatsPanelText : MonoBehaviour
{
    public TextMeshProUGUI StatsText;

    [SerializeField] private TextMeshProUGUI _moveSpeedLimit;
    [SerializeField] private TextMeshProUGUI _jumpForceLimit;

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
    public void InformAboutMoveSpeedLimit(int MoveSpeedLimit)
    {
        _moveSpeedLimit.enabled = true;
        Invoke(nameof(DisableInformationOfSpeedLimit), 2f);
    }
    public void InformAboutJumpForceLimit(int JumpForceLimit)
    {
        _jumpForceLimit.enabled = true;
        Invoke(nameof(DisableInformationOfJumpForce), 2f);
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelCompletedCanvas : MonoBehaviour
{
    public static LevelCompletedCanvas instance;
    public TextMeshProUGUI CrystalsAwardText;
    public int LevelProgress;
    [SerializeField] private Button _nextLevelButton;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance == this) Destroy(gameObject);
    }
    public void UpdateLevelProgress(int rewardCrystals)
    {
        LevelProgress = rewardCrystals;
        SetTheString();
    }
    public void SetTheString()
    {
        if (LevelProgress == 0)
            CrystalsAwardText.text = "Level completed once again<br>You rewarded " +
                LevelProgress.ToString() + " crystals.<br>Maybe try some new levels?";
        
        else if (LevelProgress == 1)
            CrystalsAwardText.text = "Well done! <br> You have passed the level! <br>You rewarded " +
                LevelProgress.ToString() + " crystal!";
        
        else if (LevelProgress == 3)
            CrystalsAwardText.text = "Nice! <br> You have passed the level! <br>You rewarded " +
                LevelProgress.ToString() + " crystal!<br>That was quite easy, wasn't it?";
        
        else if (LevelProgress == 4)
            CrystalsAwardText.text = "Nice! <br> You have passed the level! <br>You rewarded " +
                LevelProgress.ToString() + " crystal!<br><b>Don't forget to spend your crystals!</b>";
        
        else if (LevelProgress == 10)
            CrystalsAwardText.text = "Brave Hero! <br> You have passed the game! <br>You rewarded " +
                LevelProgress.ToString() + " crystal!<br>Be aware of new levels!";
       
        else 
        CrystalsAwardText.text = "Well done! <br> You have passed the level! <br>You rewarded "+
                LevelProgress.ToString() + " crystals!";
    }
    public void DisableNextLevelButton(bool enable)
    {
        _nextLevelButton.enabled = enable;
    }
}

using UnityEngine;
using TMPro;

/// <summary>
/// It controls the information a Player observes when he completes the level
/// </summary>

public class LevelCompletedCanvas : MonoBehaviour
{
    public static LevelCompletedCanvas instance;
    public TextMeshProUGUI CrystalsAwardText;
    public int levelProgress;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance == this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public void UpdateLevelProgress(int rewardCrystals)
    {
        levelProgress = rewardCrystals;
        SetTheString();
    }
    public void SetTheString()
    {
        if (levelProgress == 0)
            CrystalsAwardText.text = "Level completed once again<br>You rewarded " + levelProgress.ToString() + " crystals.<br>Maybe try some new levels?";
        else if (levelProgress == 1)
            CrystalsAwardText.text = "Well done! <br> You have passed the level! <br>You rewarded " + levelProgress.ToString() + " crystal!";
        else if (levelProgress == 3)
            CrystalsAwardText.text = "Nice! <br> You have passed the level! <br>You rewarded " + levelProgress.ToString() + " crystal!<br>That was quite easy, wasn't it?";
        else if (levelProgress == 4)
            CrystalsAwardText.text = "Nice! <br> You have passed the level! <br>You rewarded " + levelProgress.ToString() + " crystal!<br><b>Don't forget to spend your crystals!</b>";
        else if (levelProgress == 10)
            CrystalsAwardText.text = "Brave Hero! <br> You have passed the game! <br>You rewarded " + levelProgress.ToString() + " crystal!<br>Prepare for the next battles!";
        else 
        CrystalsAwardText.text = "Well done! <br> You have passed the level! <br>You rewarded "+ levelProgress.ToString() + " crystals!";
    }
}

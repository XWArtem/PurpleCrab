using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// LevelSelectCanvas controls the panel for Level selection
/// </summary>

public class LevelSelectCanvas : MonoBehaviour
{
    public Button[] levelButtons;
        
    public static LevelSelectCanvas instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance == this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        RenewLevelSelectCanvas();
    }
    public void SelectLevel (int sceneIndex)
    {
        GameManager.instance.LoadSceneOnName(sceneIndex);
    }
    public void RenewLevelSelectCanvas()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i < GameManager.instance.LevelProgress)
            {
                levelButtons[i].interactable = true;
            }
            else levelButtons[i].interactable = false;
        }
    }
}

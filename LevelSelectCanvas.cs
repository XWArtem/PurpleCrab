using UnityEngine;
using UnityEngine.UI;

public class LevelSelectCanvas : MonoBehaviour
{
    public Button[] levelButtons;
        
    public static LevelSelectCanvas instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance == this) Destroy(gameObject);
    }
    private void Start()
    {
        RenewLevelSelectCanvas();
    }
    public void SelectLevel (int sceneIndex)
    {
        GameManager.Instance.LoadSceneOnName(sceneIndex);
    }
    public void RenewLevelSelectCanvas()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i < GameManager.Instance.LevelProgress)
            {
                levelButtons[i].interactable = true;
            }
            else levelButtons[i].interactable = false;
        }
    }
}

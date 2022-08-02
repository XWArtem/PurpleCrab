using UnityEngine;
using UnityEngine.UI;

public class LevelSelectCanvas : MonoBehaviour
{
    public Button[] levelButtons;

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

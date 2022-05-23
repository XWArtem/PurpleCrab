using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    private int sceneIndex;
    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.tag == "Player")
            GameManager.instance.LevelCompleted(sceneIndex);
    }
}

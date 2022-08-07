using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class WinZone : MonoBehaviour
{
    public static Action LevelCompletedAction;

    private int sceneIndex;
    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.tag == CONSTSTRINGS.Player)
        {
            GameManager.Instance.LevelCompleted(sceneIndex);
            LevelCompletedAction?.Invoke();
        }
    }
}

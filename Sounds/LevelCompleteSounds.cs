using UnityEngine;

public class LevelCompleteSounds : MonoBehaviour
{
    [SerializeField] private AudioClip _levelCompletedClip;

    private void OnEnable()
    {
        WinZone.LevelCompletedAction += OnLevelCompletedSound;
    }
    private void OnDisable()
    {
        WinZone.LevelCompletedAction -= OnLevelCompletedSound;
    }
    private void OnLevelCompletedSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(_levelCompletedClip);
    }
}
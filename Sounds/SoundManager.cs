using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] [Header ("Audio tracks")] [Tooltip ("Add new audio track here")]
    private AudioClip[] _audioClip;

    [SerializeField] private AudioSource _audioSource;

    public void SetMusicVolume(float musicVolume)
    {
        _audioSource.volume = musicVolume;
    }
}
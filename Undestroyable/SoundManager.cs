using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] [Header ("Audio tracks")] [Tooltip ("Add new audio track here")]
    private AudioClip[] _audioClip;

    [SerializeField] private AudioSource _audioSource;
    private void Awake()
    {
        if (_audioClip[0] != null)
        {
            _audioSource.clip = _audioClip[0];
            _audioSource.Play();
        }
    }
    private void Start()
    {
        // the first track lasts 208 sec
        Invoke(nameof(SwitchTheMusic), 208f);
        _audioSource.volume = 0.0f;
    }

    private void  SwitchTheMusic()
    {
        if (_audioClip[1] != null && _audioSource.clip == _audioClip[0])
        {
            _audioSource.clip = _audioClip[1];
            _audioSource.Play();
            // the second track lasts 279 sec
            Invoke(nameof(SwitchTheMusic), 279f);
        }
        else if (_audioClip[0] != null && _audioSource.clip == _audioClip[1])
        {
            _audioSource.clip = _audioClip[0];
            _audioSource.Play();
            // the first track lasts 208 sec
            Invoke(nameof(SwitchTheMusic), 208f);
        }
    }

    public void SetMusicVolume(float musicVolume)
    {
        _audioSource.volume = musicVolume;
    }
}
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip [] _audioClip;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private bool _isPlaying;
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
        GameManager.instance.SetAudioManager(this);
        DontDestroyOnLoad(gameObject);
        Invoke("SwitchTheMusic", 208f);
        _audioSource.volume = 0.03f;
    }

    private void  SwitchTheMusic ()
    {
        if (_audioClip[1] != null && _audioSource.clip == _audioClip[0])
        {
            _audioSource.clip = _audioClip[1];
            _audioSource.Play();
            Invoke("SwitchTheMusic", 279f);
        }
        else if (_audioClip[0] != null && _audioSource.clip == _audioClip[1])
        {
            _audioSource.clip = _audioClip[0];
            _audioSource.Play();
            Invoke("SwitchTheMusic", 208f);
        }
    }

    public void SetMusicVolume(float musicVolume)
    {
        _audioSource.volume = musicVolume;
    }

}

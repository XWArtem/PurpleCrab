using UnityEngine;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerBackgroundMusic;
    [SerializeField] private AudioMixerGroup _audioMixerEffects;

    public void SetBackgroundMusicVolume(float value)
    {
        _audioMixerBackgroundMusic.audioMixer.SetFloat(CONSTSTRINGS.BackgroundMusicVolume, value);
    }
    public void SetEffectsVolume(float value)
    {
        _audioMixerEffects.audioMixer.SetFloat(CONSTSTRINGS.EffectsVolume, value);
    }
}

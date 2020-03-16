using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudioSource : MonoBehaviour
{
    static MainAudioSource _instance;
    public static MainAudioSource Instance { get { return _instance; } }

    public float Volume {
        set
        {
            if (_audioSource)
                _audioSource.volume = value;
        }
        get { return _audioSource.volume; }
    }

    [Header("My objects")]
    [SerializeField] AudioSource _audioSource;

    [Space]
    [SerializeField] AudioClip _confirmClip;
    [SerializeField] AudioClip _winClip;
    [SerializeField] AudioClip _loseClip;
    [SerializeField] AudioClip _coinClip;

    private void Awake()
    {
        _instance = this;
    }

    public void Play_ConfirmClip(float volume = 1f)
    {
        _audioSource.PlayOneShot(_confirmClip, volume);
    }

    public void Play_WinClip(float volume = 1f)
    {
        _audioSource.PlayOneShot(_winClip, volume);
    }

    public void Play_LoseClip(float volume = 1f)
    {
        _audioSource.PlayOneShot(_loseClip, volume);
    }
    
    public void Play_CoinClip(float volume = 1f)
    {
        _audioSource.PlayOneShot(_coinClip, volume);
    }

    public void Play(AudioClip clip, float volume = 1f)
    {
        _audioSource.PlayOneShot(clip, volume);
    }

    public void Stop()
    {
        _audioSource.Stop();
    }
}

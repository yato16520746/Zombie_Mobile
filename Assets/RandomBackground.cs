using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBackground : MonoBehaviour
{
    [Header("Background Music")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] List<AudioClip> _audioClips;

    [Header("Background Graphic")]
    [SerializeField] GameObject _backgroundGraphic1;
    [SerializeField] GameObject _backgroundGraphic2;

    private void Start()
    {
        // set play 1 of 3 audio Clip
        int random = Random.Range(0, _audioClips.Count);
        _audioSource.clip = _audioClips[random];
        _audioSource.Play();

        // spawn random graphic object
        int random2 = Random.Range(0, 2);
        if (random2 == 0)
        {
            Instantiate(_backgroundGraphic1);
        }
        else
        {
            Instantiate(_backgroundGraphic2);
        }
    }
}

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
        // random clip
        int random = Random.Range(0, _audioClips.Count);
        _audioSource.clip = _audioClips[random];
        _audioSource.Play();

        // random graphic
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _storyMenu;

    [Header("Audio Effect")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _confirmAudio;
    [SerializeField] AudioClip _cancelAudio;

    [Header("Scene Transition")]
    [SerializeField] LevelLoader _levelLoader;


    void Update()
    {
        
    }

    public void BUTTON_StoryMode()
    {
        _mainMenu.SetActive(false);
        _storyMenu.SetActive(true);
        _audioSource.PlayOneShot(_confirmAudio);
    }

    public void BUTTON_Back()
    {
        _mainMenu.SetActive(true);
        _storyMenu.SetActive(false);
        _audioSource.PlayOneShot(_cancelAudio);
    }

    public void BUTTON_SelectScene(string sceneName)
    {
        _levelLoader.LoadScene(sceneName);
        _audioSource.PlayOneShot(_confirmAudio);
    }
}

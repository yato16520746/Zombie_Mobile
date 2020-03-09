using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator _transition;
    [SerializeField] float _transitionTime = 1.05f;
    [SerializeField] AudioSource _mainAudioSource;

    bool _loadScene = false;
    float _count = 0;
    string _sceneName = "";

    public void LoadScene(string sceneName)
    {
        _transition.SetTrigger("End");
        _loadScene = true;
        _sceneName = sceneName;
    }

    private void Update()
    {   
        if (_loadScene)
        {
            _count += Time.deltaTime;
            if (_count > _transitionTime)
            {
                SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
            }

            _mainAudioSource.volume = 1 - _count / _transitionTime;
        }
    }
}

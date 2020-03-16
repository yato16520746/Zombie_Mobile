using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChooserButton : MonoBehaviour
{
    [Header("My objects")]
    [SerializeField] List<GameObject> _threeStars;
    [SerializeField] Button _button;
    [SerializeField] Text _text;

    int _levelNumber = 0;

    public void SetValue(int levelNumber, bool isInteractable = false, int starAmount = 0)
    {
        _levelNumber = levelNumber;
        _button.interactable = isInteractable;
        
        // stars view
        for (int i = 0; i < starAmount; i++)
        {
            _threeStars[i].SetActive(true);
        }

        // text
        _text.text = _levelNumber.ToString();
    }

    public void BUTTON_Choose()
    {
        // WARNING: Scene name have rules
        LevelLoader.Instance.LoadScene("Level " + _levelNumber.ToString());
        MainAudioSource.Instance.Play_ConfirmClip();
    }
}   

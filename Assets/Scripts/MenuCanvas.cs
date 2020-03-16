using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
    [Header("My objects")]
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _storyMenu;
    [SerializeField] List<LevelChooserButton> _levelChooserButtons;

    [Space]
    [SerializeField] Text _goldText;

    private void Start()
    {
        _mainMenu.SetActive(true);
        _storyMenu.SetActive(false);

        // get highest level
        int highestLevel = PlayerPrefs.GetInt("Highest level");
        if (highestLevel == 0)
        {
            highestLevel = 1;
            PlayerPrefs.SetInt("Highest level", highestLevel);
        }

        // set level chooser buttons
        for (int i = 0; i < _levelChooserButtons.Count; i++)
        {
            bool isInteractible = (i < highestLevel);
            int starAmount = PlayerPrefs.GetInt("Star amount in level " + (i + 1).ToString());

            _levelChooserButtons[i].SetValue(i + 1, isInteractible, starAmount);
        }

        // gold   
        int myGold = PlayerPrefs.GetInt("My gold");
        _goldText.text = myGold.ToString();
    }

    #region Buttons

    public void BUTTON_StoryMode()
    {
        _mainMenu.SetActive(false);
        _storyMenu.SetActive(true);

        MainAudioSource.Instance.Play_ConfirmClip();
    }

    public void BUTTON_Back()
    {
        _mainMenu.SetActive(true);
        _storyMenu.SetActive(false);

        MainAudioSource.Instance.Play_ConfirmClip();
    }

    public void BUTTON_Exit()
    {
        Application.Quit();

        MainAudioSource.Instance.Play_ConfirmClip();
    }

    #endregion
}

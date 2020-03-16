using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillUI : MonoBehaviour
{
    [SerializeField] Animator _animator;

    [Space]
    [SerializeField] Button _button;
    [SerializeField] GameObject _playerSkillPref;

    // price
    [Header("Price")]
    [SerializeField] int _price = 10;
    [SerializeField] Text _priceText;

    void Start()
    {
        _price = 50;

        _priceText.text = _price.ToString();
        bool isInteractable = Player.Instance.Gold >= _price;
        _button.interactable = isInteractable;
    }

    public void BUTTON_CallTitan()
    {
        // animation
        _animator.SetTrigger("Block");

        // spawn titan
        Instantiate(_playerSkillPref);

        Player.Instance.Gold -= _price;
        bool isInteractable = Player.Instance.Gold >= _price;
        _button.interactable = isInteractable;

        MainAudioSource.Instance.Play_ConfirmClip();
    }
}

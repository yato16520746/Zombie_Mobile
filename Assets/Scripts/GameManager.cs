using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text _moneyText;

    private void Start()
    {
        //PlayerPrefs.SetInt("Money", 9999);

        _moneyText.text = PlayerPrefs.GetInt("Money").ToString();
    }
}

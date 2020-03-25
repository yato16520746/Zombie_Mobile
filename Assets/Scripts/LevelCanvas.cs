using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCanvas : MonoBehaviour
{
    public static readonly int MaxLevel = 20;

    // advertise
    bool _watchAd = false;

    // singleton
    static LevelCanvas _instance;
    public static LevelCanvas Instance
    {
        get
        {
            return _instance;
        }
    }

    [Header("Level")]
    [SerializeField] int _level = 1;
    public int Level { get { return _level; } }
    [SerializeField] Text _levelText;

    [Header("For level have guild")]
    [SerializeField] GameObject _guildThePlayer;
    [SerializeField] EnemyManager _enemyManager;

    [Header("UI Pause")]
    [SerializeField] GameObject _pauseButton;
    [SerializeField] GameObject _pauseController;

    [Header("Win action")]
    [SerializeField] GameObject _winCanvas;
    [SerializeField] Animator _starsAnimator;
    [SerializeField] Text _scoreText;
    [SerializeField] Text _goldText;
    float _score = 0;
    int _gold = 0;
    float _timeCalculate = 0;
    float _totalAmount = 0; // calculate from score -> gold

    [Header("Lose action")]
    [SerializeField] GameObject _loseCanvas;
    
    void Start()
    {
        _instance = this;

        if (_guildThePlayer)
        {
            _guildThePlayer.SetActive(true);
        }

        _pauseButton.SetActive(true);
        _pauseController.SetActive(false);
        _winCanvas.SetActive(false);
        _loseCanvas.SetActive(false);

        _levelText.text = "Level " + _level;
    }  

    private void Update()
    {
        // when _score > 0, it time you are win
        if (_score > 0)
        {    
            if (_timeCalculate < 0)
            {
                float amount = 600f * Time.deltaTime;
                _score -= amount;
                if (_score < 0)
                    _score = 0;

                _scoreText.text = "Score: " + ((int)_score).ToString();
                    
                _totalAmount += amount;
                if (_totalAmount > 100f)
                {
                    // gold increase
                    _totalAmount -= 100f;
                    _gold++;
                    _goldText.text = _gold.ToString(); 

                    MainAudioSource.Instance.Play_CoinClip();
                }
            }
            else
            {
                _timeCalculate -= Time.deltaTime;
            }
        }
    }

    public void Win()
    {
        AdController.instance.ShowVideoAD();

        if (_winCanvas)
            _winCanvas.SetActive(true);

        if (_starsAnimator)
            _starsAnimator.SetInteger("Number", Player.Instance.Health);
        _timeCalculate = Player.Instance.Health / 2;

        _score = Player.Instance.Score;
        _scoreText.text = "Score: " + _score;

        MainAudioSource.Instance.Stop();
        MainAudioSource.Instance.Play_WinClip();

        // save gold
        int myGold = PlayerPrefs.GetInt("My gold");
        if (_watchAd) // x3 gold for Ad
        {
            myGold += ((int)_score / 100) * 3;
        }
        else
        {
            myGold += (int)_score / 100;
        }
       
        PlayerPrefs.SetInt("My gold", myGold);

        // save highest level
        int highestLevel = PlayerPrefs.GetInt("Highest level");
        if (highestLevel < MaxLevel) // finish <= 18 level
        {
            if (_level == highestLevel) // currently in highest level
            {
                PlayerPrefs.SetInt("Highest level", highestLevel + 1);
 
            }
        }

        // save stars
        int currentStars = PlayerPrefs.GetInt("Star amount in level " + _level.ToString());
        if (currentStars < Player.Instance.Health)
        {
            PlayerPrefs.SetInt("Star amount in level " + _level.ToString(), Player.Instance.Health);
        }

       
    }

    public void Lose()
    {
        AdController.instance.ShowVideoAD();

        _loseCanvas.SetActive(true);
        Time.timeScale = 0;

        MainAudioSource.Instance.Stop();
        MainAudioSource.Instance.Play_LoseClip();


    }

    #region Buttons

    public void Destroy_GuildThePlayer()
    {
        if (_guildThePlayer)
        {
            Destroy(_guildThePlayer);
            _enemyManager.Unlock();
            //_mainAudioSource.PlayOneShot(_confirmClip);
        }
    }

    public void BUTTON_Pause()
    {
        _pauseButton.SetActive(false);
        _pauseController.SetActive(true);

        MainAudioSource.Instance.Play_ConfirmClip();

        Time.timeScale = 0;
    }

    public void BUTTON_Resume()
    {
        _pauseButton.SetActive(true);
        _pauseController.SetActive(false);

        MainAudioSource.Instance.Play_ConfirmClip();

        Time.timeScale = 1;
    }

    public void BUTTON_Restart()
    {
        LevelLoader.Instance.LoadCurrentScene();

        MainAudioSource.Instance.Play_ConfirmClip();
    }

    public void BUTTON_Exit()
    {
        LevelLoader.Instance.LoadScene("Menu");

        MainAudioSource.Instance.Play_ConfirmClip();
    }

    public void BUTTON_NextLevel()
    {
        // WARNING: Level scene name have rules
        LevelLoader.Instance.LoadScene("Level " + (_level + 1).ToString());

        MainAudioSource.Instance.Play_ConfirmClip();
    }

    #endregion
}

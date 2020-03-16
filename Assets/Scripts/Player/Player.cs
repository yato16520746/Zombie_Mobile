using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // singleton
    static Player _instance;
    public static Player Instance
    {
        get { return _instance; }
    }

    // touch
    [Header("Touch")]
    [SerializeField] float _range = 30f;
    [SerializeField] int _shootableMask;
    Ray _touchRay;
    RaycastHit _touchHit;
    int combo;
    bool _hold = false;
    Monster _golem;

    // gold
    [Header("Gold")]
    [SerializeField] Text _goldText;
    int _gold;
    public int Gold
    {
        set
        {
            _gold = value;
            _goldText.text = _gold.ToString();

            PlayerPrefs.SetInt("My gold", _gold); // save
        }
        get
        {
            return _gold;
        }
    }

    // health
    [Header("Health")]
    [SerializeField] List<Image> _healthImages;
    int _health;
    public int Health
    {
        get
        {
            return _health;
        }
    }

    // score
    [Header("Score")]
    [SerializeField] Text _scoreText;
    float _score;
    public int Score
    {
        get
        {
            return (int)_score;
        }
    }

    // start
    private void Awake()
    {
        // singleton
        _instance = this;

        // get mask to touch
        _shootableMask = LayerMask.GetMask("CollideEnvironment");

        // combo count
        combo = 0;

        // load gold
        _gold = PlayerPrefs.GetInt("My gold");
        _goldText.text = _gold.ToString();

        // health
        _health = 3;

        // score
        _score = 0f;
        _scoreText.text = "Score: " + _score.ToString();
    }

    // update
    void Update()
    {
        // touch
        if (Input.GetMouseButtonDown(0))
        {
            _touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_touchRay, out _touchHit, _range, _shootableMask))
            {
                Monster monster = _touchHit.collider.gameObject.GetComponent<Monster>();
                monster.AddDamage(_touchHit.point);

                if (monster is Golem)
                {
                    _hold = true;
                    _golem = monster;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _hold = false;
        }

        if (_hold && _golem)
        {
            _golem.AddDamage(Vector3.zero);
        }
    }

    // add damage
    public void AddDamage()
    {
        // already dead ?
        if (_health <= 0)
        {
            return;
        }

        _health--;
        if (_health >= 0)
        {
            // heart anim
            Animator healthAnim = _healthImages[2 - _health].gameObject.GetComponent<Animator>();
            healthAnim.SetTrigger("Break");

            // vibrate device
            Handheld.Vibrate();
        }

        // lose
        if (_health == 0)
        {
            LevelCanvas.Instance.Lose();
        }
    }

    // add score
    public void AddScore(int score)
    {
        _score += score;
        if (_scoreText)
        {
            _scoreText.text = "Score: " + _score.ToString();
        }
    }

    // trigger -> add damage
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            AddDamage();
        }
    }
}

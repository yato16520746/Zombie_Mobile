using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject _boomPrefab;

    [SerializeField] float _range = 30f;
    [SerializeField] int _shootableMask;
    Ray _touchRay;
    RaycastHit _touchHit;

    [Header("Character property")]
    [SerializeField] Text _moneyText;
    int _money;

    [SerializeField] List<Image> _healthImages;
    int _health;

    private void Start()
    {
        _shootableMask = LayerMask.GetMask("CollideEnvironment");

        // money
        _money = PlayerPrefs.GetInt("Money");
        _moneyText.text = _money.ToString();

        // health
        _health = _healthImages.Count;
    }

    void Update()
    {
        // Player Tap
        if (Input.GetMouseButtonDown(0))
        {
            _touchRay = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_touchRay, out _touchHit, _range, _shootableMask))
            {
                Monster monster = _touchHit.collider.gameObject.GetComponent<Monster>();
                monster.AddDamage(_touchHit.point);
            }
        }
    }

    public void VibrateMyPhone()
    {
        Handheld.Vibrate();
    }

    public void AddDamage()
    {
        _health--;
        if (_health >= 0)
        {
            Animator healthAnim = _healthImages[2 - _health].gameObject.GetComponent<Animator>();
            healthAnim.SetTrigger("Break");
        }

        if (_health == 0)
        {
            Debug.Log("Lose!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            AddDamage();
        }
    }
}

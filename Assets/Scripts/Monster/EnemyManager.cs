using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [Header("On Validate")]
    [SerializeField] MonsterWave _currentMonsterWave;
    [SerializeField] List<MonsterWave> _monsterWaves; // manage many Monster Wave

    [Header("UI mechanic")]
    [SerializeField] bool _isLock = false;
    [SerializeField] Slider _waveSlider;
    float _waveValue;

    private void OnValidate()
    {
        _monsterWaves.Clear();
        foreach (MonsterWave mw in GetComponentsInChildren<MonsterWave>(true))
        {
            _monsterWaves.Add(mw);
        }

        _currentMonsterWave = _monsterWaves[0];
    }

    private void Start()
    {
        foreach (MonsterWave mw  in _monsterWaves)
        {
            mw.gameObject.SetActive(false);
            mw.SetEnemyManager(this);
        }

        if (!_isLock)
            _currentMonsterWave.gameObject.SetActive(true);

        // set wave slider
        _waveSlider.maxValue = _monsterWaves.Count;
        _waveValue = _monsterWaves.Count;
        _waveSlider.value = _monsterWaves.Count;
    }

    private void Update()
    {
        if (_waveSlider)
            _waveSlider.value = Mathf.Lerp(_waveSlider.value, _waveValue, 5 * Time.deltaTime);
    }

    public void RemoveWave(MonsterWave monsterWave)
    {
        _monsterWaves.Remove(monsterWave);

        // set value of the wave slider
        _waveValue -= 1;
        if (_waveValue < 0)
            _waveValue = 0;

        if (_monsterWaves.Count > 0)
        {
            _currentMonsterWave = _monsterWaves[0];
            _currentMonsterWave.gameObject.SetActive(true);
        }
        else
        {
            // player win
            LevelCanvas.Instance.Win();
        }
    }

    public void Unlock()
    {
        if (_isLock)
        {
            _isLock = false;
            _currentMonsterWave = _monsterWaves[0];
            _currentMonsterWave.gameObject.SetActive(true);
        }
    }
}

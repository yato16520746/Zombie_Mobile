using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("On Validate")]
    [SerializeField] MonsterWave _currentMonsterWave;
    [SerializeField] List<MonsterWave> _monsterWaves; // manage many Monster Wave

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

        _currentMonsterWave.gameObject.SetActive(true);
    }

    public void RemoveWave(MonsterWave monsterWave)
    {
        _monsterWaves.Remove(monsterWave);
        if (_monsterWaves.Count > 0)
        {
            _currentMonsterWave = _monsterWaves[0];
            _currentMonsterWave.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Level Completed");
        }
    }
}

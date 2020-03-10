using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWave : MonoBehaviour
{
    [SerializeField] List<GameObject> _monsterPrefs;
    [SerializeField] List<float> _monsterPercents;
    float _totalPercent;

    [Header("Wave Property")]
    // number of Monster in this Wave
    [SerializeField] int _minMonsterAmount;
    [SerializeField] int _maxMonsterAmount;
    int _monsterAmount;
    int _stillAlive;

    [SerializeField] int _maxMonsterInRow;
    [SerializeField] float _minTimeBetweenRows, _maxTimeBetweenRows;
    float _timeBetweenRows;

    [SerializeField] float _minMoveSpeed;
    [SerializeField] float _maxMoveSpeed;

    float _timeCount = 0;

    EnemyManager _enemyManager;

    private void Start()
    {
        if (_monsterPrefs.Count != _monsterPercents.Count)
        {
            Debug.LogError("Monster Spawn Machine bug!");
        }

        // get the exact number of monster in this Wave
        _monsterAmount = Random.Range(_minMonsterAmount, _maxMonsterAmount + 1);
        _stillAlive = _monsterAmount;

        // random a time between rows
        _timeBetweenRows = Random.Range(_minTimeBetweenRows, _maxTimeBetweenRows);

        // get total of monster spawn percent
        _totalPercent = 0;
        for (int i = 0; i < _monsterPercents.Count; i++)
        {
            _totalPercent += _monsterPercents[i];
        }
    }

    private void Update()
    {
        _timeCount += Time.deltaTime;
        if (_monsterAmount > 0 && _timeCount > _timeBetweenRows)
        {
            _timeCount = 0;

            // get number of monster in row
            int monsterInRow = Random.Range(1, _maxMonsterInRow + 1);
            if (monsterInRow > _monsterAmount)
            {
                monsterInRow = _monsterAmount;
            }
            _monsterAmount -= monsterInRow;

            // positions already used in this row => for random position
            List<int> usedPositions = new List<int>();
            usedPositions.Clear();

            // spawn all monster in this row
            for (int i = 0; i < monsterInRow; i++)
            {
                // random a type of monster
                float randomPercent = Random.Range(0, _totalPercent);
                float countPercent = 0;
                int type = -1;

                for (int j = 0; j < _monsterPercents.Count; j++)
                {
                    countPercent += _monsterPercents[j];

                    if (randomPercent < countPercent) // spawn this monster, then break
                    {
                        type = j;
                        break;
                    }
                }

                // get random position for this monster
                bool checkUsed = true;
                int randomPosition = -1;

                while (checkUsed)
                {
                    checkUsed = false;
                    randomPosition = Random.Range(0, 5);
                    foreach (int pos in usedPositions)
                    {
                        if (randomPosition == pos)
                        {
                            checkUsed = true;
                        }
                    }
                }
                usedPositions.Add(randomPosition);

                Vector3 position = new Vector3(-8 + randomPosition * 4, transform.position.y, transform.position.z);

                // set random offset of this position
                float randomX = Random.Range(-1, 1);
                float randomY = Random.Range(-1, 1);
                if (randomX != 0 && randomY != 0)
                {
                    Vector2 offset = new Vector2(randomX, randomY);
                    offset = offset.normalized * Random.Range(0.0f, 1.3f);
                    position.x += offset.x;
                    position.z += offset.y;
                }

                GameObject newMonster = Instantiate(_monsterPrefs[type], position, Quaternion.identity);

                newMonster.GetComponent<Monster>().SetMonsterProperty(Random.Range(_minMoveSpeed, _maxMoveSpeed), this);
            }   
        }
    }

    public void SetEnemyManager(EnemyManager manager)
    {
        _enemyManager = manager;
    }

    public void OnAMonsterDestroy()
    {
        _stillAlive -= 1;
        if (_stillAlive <= 0)
        {
            Destroy(gameObject);
            _enemyManager.RemoveWave(this);
        }
    }
}

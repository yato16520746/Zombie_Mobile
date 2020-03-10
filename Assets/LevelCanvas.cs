    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCanvas : MonoBehaviour
{
    [Header("For level have guild")]
    [SerializeField] GameObject _guildThePlayer;
    [SerializeField] EnemyManager _enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destroy_GuildThePlayer()
    {
        if (_guildThePlayer)
        {
            Destroy(_guildThePlayer);
            _enemyManager.Unlock();
        }
    }
}

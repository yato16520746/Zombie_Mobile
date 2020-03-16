using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [Header("On Validate")]
    [SerializeField] protected Rigidbody _rb;

    [Header("Property")]
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected MonsterWave _monsterWave;

    [SerializeField] int _maxHealth = 1;
    int _currentHealth;

    [SerializeField] protected GameObject _deathEffecetPref;

    [Header("Interact with player")]
    [SerializeField] int _score = 10;
    [SerializeField] protected List<AudioClip> _deadClips;

    protected virtual void Start()
    {
        _currentHealth = _maxHealth;
    }

    protected virtual void OnValidate()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void SetMonsterProperty(float speed, MonsterWave wave)
    {
        _moveSpeed = speed;
        _monsterWave = wave;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.Player_Attack)
        {
            _currentHealth -= 1;

            if (_currentHealth <= 0)
            {
                Create_Ragdoll();
                Destroy(gameObject);
            }
            else
            {
                HitEffect();
            }
        }
    }

    public virtual void AddDamage(Vector3 attackHitPoint)
    {
        _currentHealth -= 1;
        if (_currentHealth <= 0)
        {
            Create_Ragdoll();
            
            // death effect
            Instantiate(_deathEffecetPref, transform.position, Quaternion.identity);

            MainAudioSource.Instance.Play(_deadClips[Random.Range(0, _deadClips.Count)]);
            Destroy(gameObject);
        }
        else
        {
            HitEffect();
        }
    }

    public virtual void HitDeathSkill()
    {
        _currentHealth = 0;
        Create_Ragdoll();

        // death effect
        Instantiate(_deathEffecetPref, transform.position, Quaternion.identity);

        MainAudioSource.Instance.Play(_deadClips[Random.Range(0, _deadClips.Count)]);
        HitEffect();
        Destroy(gameObject);
    }

    protected abstract void Create_Ragdoll();

    private void OnDestroy()
    {
        Player.Instance.AddScore(_score);

        if (_monsterWave)
            _monsterWave.OnAMonsterDestroy();
        else
            Debug.Log("BUG! Monster is not belong to any Wave.");
    }

    protected virtual void HitEffect()
    {

    }
}

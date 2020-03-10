﻿using System.Collections;
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

    [SerializeField] GameObject _deathEffecetPref;

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

    public void AddDamage(Vector3 attackHitPoint)
    {
        _currentHealth -= 1;
        if (_currentHealth <= 0)
        {
            Create_Ragdoll();
            Instantiate(_deathEffecetPref, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            HitEffect();
        }
    }

    protected abstract void Create_Ragdoll();

    private void OnDestroy()
    {
        if (_monsterWave)
            _monsterWave.OnAMonsterDestroy();
        else
            Debug.Log("BUG! Monster is not belong to any Wave.");
    }

    protected virtual void HitEffect()
    {

    }
}

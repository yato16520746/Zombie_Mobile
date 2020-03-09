using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Monster
{
    [Header("On Editor")]
    [SerializeField] Animator _animator;

    [Space]
    [SerializeField] eDirection _eDirection;

    Vector3 _direction = new Vector3(0, 0, -1);
    private static readonly string Die_Trigger = "Die"; // for animation

    #region Override function
    protected override void Start()
    {
        base.Start();

        if (_eDirection == eDirection.GoLeft)
        {
            _direction.x = Random.Range(-0.9f, -0.7f);
        }
        else if (_eDirection == eDirection.GoRight)
        {
            _direction.x = Random.Range(0.7f, 0.9f);
        }
    }

    protected override void HitEffect()
    {
        // you gonna lose this game
        Debug.Log("Lose");
        _animator.SetTrigger(Die_Trigger);
        _moveSpeed = 0;
    }
    #endregion

    void Update()
    {
        // apply velocity
        _rb.velocity = _direction.normalized * _moveSpeed;
    }

    protected override void Create_Ragdoll()
    {
        // Just set animation Death
        _moveSpeed = 0;
        _animator.SetTrigger(Die_Trigger);
    }
}

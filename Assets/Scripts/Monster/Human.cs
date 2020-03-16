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

    bool _lose = false;
    float _count = 0.9f;


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
        _animator.SetTrigger(Die_Trigger);
        _moveSpeed = 0;
    }
    #endregion

    void Update()
    {
        // apply velocity
        _rb.velocity = _direction.normalized * _moveSpeed;

        // look rotation
        Quaternion quaternion = Quaternion.LookRotation(_direction);
        _animator.transform.rotation = Quaternion.Lerp(_animator.transform.rotation, quaternion, 3f * Time.deltaTime);

        if (_lose)
        {
            _count -= Time.deltaTime;
            if (_count < 0)
            {
                LevelCanvas.Instance.Lose();
                _lose = false;
            }
        }
    }

    protected override void Create_Ragdoll()
    {
        // Just set animation Death
        _moveSpeed = 0;
        _animator.SetTrigger(Die_Trigger);
    }

    public override void AddDamage(Vector3 attackHitPoint)
    {
        base.AddDamage(attackHitPoint);
        _lose = true;
        MainAudioSource.Instance.Play(_deadClips[Random.Range(0, _deadClips.Count)]);
    }

    public override void HitDeathSkill()
    {
     
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tags.Wall)
        {
            _direction.x = -_direction.x;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Monster
{
    [Header("On Validate")]
    [SerializeField] Animator _animator;

    [Header("Ragdoll")]
    [SerializeField] GameObject _ragdollPref;
    [SerializeField] Transform _graphicRoot;

    float _hitDuration = -0.1f;
    float _originMoveSpeed;

    #region Override function
    protected override void Start()
    {
        base.Start();
        _originMoveSpeed = 2f + _moveSpeed / 5f;
        _moveSpeed = _originMoveSpeed;
    }

    protected override void OnValidate()
    {
        base.OnValidate();

        _animator = GetComponentInChildren<Animator>();
    }

    protected override void Create_Ragdoll()
    {
        GameObject ragdoll = Instantiate(_ragdollPref, _graphicRoot.position, _graphicRoot.rotation);
        Ragdoll mumRagdoll = ragdoll.GetComponent<Ragdoll>();
        mumRagdoll.SetUp_Small(2f, _graphicRoot, true);
    }

    protected override void HitEffect()
    {
        if (Random.Range(0, 2) == 0)
        {
            _hitDuration = 0.3f;
            _animator.SetBool("Hit", true);
        }
    }
    #endregion

    void Update()
    {
        if (_hitDuration < 0)
        {
            _moveSpeed = Mathf.Lerp(_moveSpeed, _originMoveSpeed, 6f * Time.deltaTime);
        }
        else
        {
            _hitDuration -= Time.deltaTime;

            _moveSpeed = Mathf.Lerp(_moveSpeed, _originMoveSpeed / 2f, 6f * Time.deltaTime);
        }

        _rb.velocity = new Vector3(0, 0, -1) * _moveSpeed;
    }

    private void LateUpdate()
    {
        _animator.SetBool("Hit", false);
    }


}

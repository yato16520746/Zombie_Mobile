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
    float _originMoveSpeed;
    Vector3 _originScale;

    // smaller
    [Header("Smaller")]
    bool _smaller = false;

    #region Override function
    protected override void Start()
    {
        base.Start();
        _originMoveSpeed = 2f + _moveSpeed / 2f;
        _moveSpeed = _originMoveSpeed;
        _originScale = _graphicRoot.transform.localScale;
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
        mumRagdoll.SetUp_NoPushForce(_graphicRoot, _graphicRoot.transform.localScale);
    }

    protected override void HitEffect()
    {

    }
    #endregion

    void Update()
    {
        if (!_smaller)
        {
            _moveSpeed = Mathf.Lerp(_moveSpeed, _originMoveSpeed, 6f * Time.deltaTime);

            _graphicRoot.transform.localScale = Vector3.Lerp(_graphicRoot.transform.localScale, _originScale, 1.2f * Time.deltaTime);
        }
        else
        {
            _moveSpeed = Mathf.Lerp(_moveSpeed, _originMoveSpeed / 5f, 6f * Time.deltaTime);

            _graphicRoot.transform.localScale = Vector3.Lerp(_graphicRoot.transform.localScale, new Vector3(9.5f, 9.5f, 9.5f), 1.2f * Time.deltaTime);
            if (_graphicRoot.transform.localScale.x < 10f)
            {
                Dead();
            }
        }

        _rb.velocity = new Vector3(0, 0, -1) * _moveSpeed;

        _smaller = false;
    }

    private void LateUpdate()
    {
        _animator.SetBool("Hit", false);
    }

    public override void AddDamage(Vector3 attackHitPoint)
    {
        _smaller = true;
    }

    void Dead()
    {
        Create_Ragdoll();

        // death effect
        Instantiate(_deathEffecetPref, transform.position, Quaternion.identity);

        MainAudioSource.Instance.Play(_deadClips[Random.Range(0, _deadClips.Count)]);

        Destroy(gameObject);
    }
}

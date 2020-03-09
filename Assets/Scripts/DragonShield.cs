using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonShield : Monster
{
    [Header("On Validate")]
    [SerializeField] Animator _animator;

    [Header("Ragdoll")]
    [SerializeField] GameObject _ragdollPref;
    [SerializeField] Transform _graphicRoot;

    [Header("Hit Effect")]
    [SerializeField] GameObject _shield;
    [SerializeField] GameObject _shieldRagdollPref;

    // Dragon Shield change velocity when he lost his shield
    Vector3 _direction = new Vector3(0, 0, -1);
    bool _lostShield = false;

    private static readonly string LostShield = "Lost Shield"; // for animation

    #region Override function
    protected override void OnValidate()
    {
        base.OnValidate();
        _animator = GetComponentInChildren<Animator>();
    }

    protected override void Create_Ragdoll()
    {
        GameObject ragdoll = Instantiate(_ragdollPref, _graphicRoot.position, _graphicRoot.rotation);
        Ragdoll mumRagdoll = ragdoll.GetComponent<Ragdoll>();
        mumRagdoll.SetUp(4f, _graphicRoot);
    }

    protected override void HitEffect()
    {
        if (_shield)
        {
            // change animation
            _animator.SetBool(LostShield, true);

            // change direction
            int random = Random.Range(0, 3);
            if (random == 0)
            {
                _direction.x = Random.Range(-0.9f, -0.6f);
            }
            else if (random == 2)
            {
                _direction.x = Random.Range(0.6f, 0.9f);
            }

            // speed up movement
            _moveSpeed *= 1.3f;

            // spawn shield ragdoll
            Destroy(_shield);
            Instantiate(_shieldRagdollPref, _shield.transform.position, _shield.transform.rotation);
        }
    }
    #endregion

    void Update()
    {
        if (!_shield)
        {
            // look rotation
            Quaternion quaternion = Quaternion.LookRotation(_direction);
            _animator.transform.rotation = Quaternion.Lerp(_animator.transform.rotation, quaternion, 3f * Time.deltaTime);
        }

        // apply velocity
        _rb.velocity = _direction.normalized * _moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tags.Wall)
        {
            _direction.x = -_direction.x;
        }
    }
}

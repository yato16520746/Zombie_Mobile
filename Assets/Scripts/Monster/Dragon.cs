using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Monster
{
    [Header("On Validate")]

    [SerializeField] Animator _animator;

    [Header("Ragdoll")]
    [SerializeField] GameObject _ragdollPref;
    [SerializeField] Transform _graphicRoot;

    // Dragon change velocity whem time come
    Vector3 _direction = new Vector3(0, 0, -1);
    float _timeChangeDir;
    bool _changed = false;

    float _time = 0; // counting time when Dragon spawned

    private static readonly string Run = "Run";

    #region Override function
    protected override void Start()
    {
        base.Start();

        _timeChangeDir = Random.Range(12f, 32f) / _moveSpeed;
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
        mumRagdoll.SetUp(4f, _graphicRoot);
    }
    #endregion

    private void Update()
    {
        // check the time: Dragon changing velocity
        if (!_changed)
        {
            _time += Time.deltaTime;

            if (_time > _timeChangeDir)
            {
                _changed = true;

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

                _animator.SetBool(Run, true);
            }
        }
        else
        {
            // look rotation
            Quaternion quaternion = Quaternion.LookRotation(_direction);
            _animator.transform.rotation = Quaternion.Lerp(_animator.transform.rotation, quaternion, 3f * Time.deltaTime);
        }

        // apply velocity over time
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

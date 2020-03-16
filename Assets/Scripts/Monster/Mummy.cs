using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mummy : Monster
{
    [Header("Auto Skin Color")]
    [SerializeField] List<Color> _skinColors;
    [SerializeField] List<GameObject> _graphicGameObjects;
    Color _myColor;

    [Header("Ragdoll")]
    [SerializeField] GameObject _ragdollPref;
    [SerializeField] Transform _graphicRoot;

    [Header("Movement")]
    [SerializeField] eDirection _eDirection;
    Vector3 _direction = new Vector3(0, 0, -1);
    [SerializeField] Animator _animator;

    protected override void Start()
    {
        base.Start();

        // set random skin color for mummy
        int random = Random.Range(0, _skinColors.Count);
        _myColor = _skinColors[random];

        foreach (GameObject graphicGO in _graphicGameObjects)
        {
            foreach (Material material in graphicGO.GetComponent<Renderer>().materials)
            {
                material.SetColor("_Color", _myColor);
            }
        }

        // set direction
        if (_eDirection == eDirection.GoLeft)   
        {
            _direction.x = Random.Range(-0.9f, -0.7f);
            _moveSpeed *= 1.2f;
        }
        else if (_eDirection == eDirection.GoRight)
        {
            _direction.x = Random.Range(0.7f, 0.9f);
            _moveSpeed *= 1.2f;
        }
    }

    private void Update()
    {
        // apply velocity alway go down
        _rb.velocity = _direction.normalized * _moveSpeed;

        // look rotation
        Quaternion quaternion = Quaternion.LookRotation(_direction);
        _animator.transform.rotation = Quaternion.Lerp(_animator.transform.rotation, quaternion, 3f * Time.deltaTime);
    }


    #region Override function   
    protected override void OnValidate()
    {
        base.OnValidate();
    }

    protected override void Create_Ragdoll()
    {
        GameObject ragdoll = Instantiate(_ragdollPref, _graphicRoot.position, _graphicRoot.rotation);
        Ragdoll mumRagdoll = ragdoll.GetComponent<Ragdoll>();
        mumRagdoll.SetUp(4f, _graphicRoot, _myColor);
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tags.Wall)
        {
            _direction.x = -_direction.x;
        }
    }
}
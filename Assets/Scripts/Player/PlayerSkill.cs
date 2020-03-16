using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _moveSpeed = 10f;

    private void Update()
    {
        _rb.velocity = new Vector3(0, 0, _moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Monster monster = other.GetComponent<Monster>();
            monster.HitDeathSkill();
        }
    }
}

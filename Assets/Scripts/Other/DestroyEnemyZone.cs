using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Human")
            Destroy(other.gameObject);
    }
}

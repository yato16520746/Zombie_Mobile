using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    [SerializeField] float count_Destroy = 1.0f;

    void Update()
    {
        count_Destroy -= Time.deltaTime;

        if (count_Destroy < 0)
            Destroy(gameObject);
    }
}

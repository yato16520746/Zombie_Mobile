using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotaton : MonoBehaviour
{
    [SerializeField] float speed = 3;
   

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed, 0);
    }
}

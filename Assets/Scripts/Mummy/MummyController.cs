using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyController : MonoBehaviour
{   
    [SerializeField] float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }
    public Vector3 Direction = new Vector3(0, 0, 1);
}

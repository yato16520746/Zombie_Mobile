using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    [SerializeField] float walkSpeed = 5;
    public float WalkSpeed { get { return walkSpeed; } }

    [SerializeField] float runSpeed = 10;
    public float RunSpeed { get { return runSpeed; } }

    [SerializeField] Vector3 direction = new Vector3(0, 0, 1);
    public Vector3 Direction { get { return direction; } }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMoveController : MonoBehaviour
{
    protected Vector3 playerposition;
    protected Rigidbody rig;
    void Awake()
    {
        playerposition = transform.position;
        PositionInitialization();
    }

    public abstract void PositionInitialization();
    public abstract void Movement();
}

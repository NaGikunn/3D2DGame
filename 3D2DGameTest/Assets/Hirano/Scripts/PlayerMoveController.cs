using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMoveController : MonoBehaviour
{
    protected PlayerManagerController Manager;
    protected Vector3 playerposition;
    protected Animator anim;
    protected Rigidbody rig;
    protected float flap = 250.0f;
    protected bool Jump = false;
    void Awake()
    {
        playerposition = transform.position;
        PositionInitialization();
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        Manager = GetComponent<PlayerManagerController>();
    }

    public abstract void PositionInitialization();
    public abstract void Movement();

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Stage"))
        {
            Jump = false;
            anim.SetBool("Jump", false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        Manager = GetComponent<PlayerManagerController>();

        PositionInitialization();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Gole"))
        {
            Manager.ClearLabel.SetActive(true);
            Invoke("StageSlect", 2.0f);
        }
    }
 
     void StageSlect()
    {
        SceneManager.LoadScene("StageSelect");
    }
}

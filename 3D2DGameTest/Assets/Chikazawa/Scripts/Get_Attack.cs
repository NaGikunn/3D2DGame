using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Attack : MonoBehaviour
{

    //攻撃判定
    public bool AttackFlg = false;
    public bool GetAttack = false;
    public StateMachine.Enemy enemy;

    public bool AttackStopflg;
    float AttackStopTime;

    //   // Use this for initialization
    //   void Start () {

    //}

    //// Update is called once per frame
    void Update()
    {
        if (GetAttack && AttackFlg && !AttackStopflg && !enemy.IsFly)
        {
            Debug.Log("ATTACK_HIT!!");
            GetAttack = false;
        }
        if (AttackStopflg)
        {
            if (AttackStopTime >= 3f)
            {
                AttackStopflg = false;
                Debug.Log("ATTACK READY");
                AttackStopTime = 0;
            }
            AttackStopTime += Time.deltaTime;
            //Debug.Log(AttackStopTime);
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
            ChangeFlg();
    }

    //void OnTriggerStay2D(Collider2D collider)
    //{

    //}

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            ChangeFlg();
            GetAttack = false;
        }
        //if (AttackStopflg)
        //{
        //    AttackStopflg = false;
        //    Debug.Log("ATTACK READY");
        //}

    }
    void ChangeFlg()
    {
        AttackFlg = !AttackFlg;
    }
}

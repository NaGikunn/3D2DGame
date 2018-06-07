using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Pursuit : MonoBehaviour {

    //追跡用判定
    public bool PursuitFlg = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
            ChangeFlg();
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            ChangeFlg();
        }
    }
    void ChangeFlg()
    {
        PursuitFlg = !PursuitFlg;
    }
}

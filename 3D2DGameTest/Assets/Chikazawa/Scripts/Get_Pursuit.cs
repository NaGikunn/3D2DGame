using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Pursuit : MonoBehaviour {

    //追跡用判定
    public bool PursuitFlg = false;

    public StateMachine.Enemy enemy;
    
    Ray ray;
    Vector3 DirecthionToPlayer;

    // outパラメータ用に、Rayのヒット情報を取得するための変数を用意
    RaycastHit hit;
    public string hitTag;
    //int LeyerMask=

    void Update()
    {
        if(PursuitFlg )
        DirecthionToPlayer = enemy.player.transform.position - enemy.gameObject.transform.position;

        // とりあえずPlayerに向かってRayを飛ばす（第1引数がRayの発射座標、第2引数がRayの向き）
        ray = new Ray(enemy.gameObject.transform.position, DirecthionToPlayer);


        // シーンビューにRayを可視化
        Debug.DrawRay(ray.origin, ray.direction * 19.0f, Color.red, 0.0f);

        // Rayのhit情報を取得する(レイ、衝突したオブジェクトの情報、長さ、レイヤー)
        if (Physics.Raycast(ray, out hit, 19.0f))
        {

            // Rayがhitしたオブジェクトのタグ名を取得
            hitTag = hit.collider.tag;
            //Debug.Log(hitTag);
        }
    }
        //
        void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
            ChangeFlg(); 
    }
    //
    void OnTriggerExit(Collider collider)
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

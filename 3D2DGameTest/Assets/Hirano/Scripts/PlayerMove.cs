using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    CameraControl Cameracontrol;
    //プレイヤーのアニメーター
    Animator anim;
    //プレイヤーのキャラクターコントローラ
    CharacterController controller;
    //プレイヤーの動くスピード
    float speed = 3.0f;
    //Horizontal
    float px = 0.0f;
    //Vertical
    float pz = 0.0f;
    //プレイヤーの位置
    Vector3 playerPos;
    // Use this for initialization
    void Start ()
    {
        controller = GetComponent<CharacterController>();
        playerPos = transform.position;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        //カメラの方向からx-z平面の単位のベクトルを取得
        Vector3 cameraFoward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveFoward = cameraFoward * v + Camera.main.transform.right * h;
        var inputAxis = moveFoward;
        Cameracontrol.MoveCamera();
        PlayerManager(inputAxis);
    }

    //プレイヤーの動き
    void PlayerManager(Vector3 moveDirection)
    {
        var deltaSpeed = moveDirection * speed * Time.deltaTime;
        //プレイヤーの移動
        controller.Move(deltaSpeed);

        var isInput = moveDirection.magnitude >= 0.1;

        //ベクトルの長さが0.01fより大きい場合にプレイヤーの方向を変える処理を入力
        if (isInput)
        {
            var degree = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.up * degree);
        }
        //プレイヤーの位置を更新
        playerPos = transform.position;

        //左スティックから入力がありそれが0.1f以上ならアニメーションを再生
        if (isInput)
        {
            //歩くアニメーション
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    CameraControl Cameracontrol;
    //プレイヤーのアニメーター
    Animator anim;
    //プレイヤーのRigidbody
    Rigidbody playerRig;
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
        playerRig = GetComponent<Rigidbody>();
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
        playerRig.velocity = new Vector3(h, 0, v);
    }
}

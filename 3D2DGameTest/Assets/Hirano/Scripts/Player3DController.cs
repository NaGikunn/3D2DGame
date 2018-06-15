using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DController : PlayerMoveController
{
    public override void PositionInitialization()
    {
        playerposition = new Vector3(0, 0.5f, -3);
    }

    public override void Movement()
    {
        var speed = 3.0f;
        var h = Input.GetAxis("Horizontal") * speed;
        var v = Input.GetAxis("Vertical") * speed;
        var AxisInput = new Vector3(h, 0.0f, v);
        transform.position -= AxisInput * Time.deltaTime;
        //回転
        //var degree = Mathf.Atan2(AxisInput.x, AxisInput.z) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(Vector3.down * degree);
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            transform.rotation = Quaternion.LookRotation(transform.position -
            (Vector3.right * Input.GetAxisRaw("Horizontal")) -
            (Vector3.forward * Input.GetAxisRaw("Vertical"))
            - transform.position);
        }
        //ジャンプ
        //BFFALO　3番キーを押したら
        if (Input.GetButtonDown("JoyStick2") && !Jump)
        {
            rig.AddForce(Vector2.up * flap);
            anim.SetBool("Jump",true);
            anim.SetBool("walk", false);
            Jump = true;
        }
        //アニメーション
        bool MoveInput = AxisInput.magnitude >= 0.1f;
        //左スティックから入力がありそれが0.1f以上ならアニメーションを再生
        if (MoveInput)
        {
            //歩くアニメーション
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }
        //BFFALO　２番キーを押したら
        if (Input.GetButtonDown("JoyStick1"))
        {
            Manager.GController.ChangeDimension();
        }
        //救済措置
        if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.position = new Vector3(0, 1, 0);
        }
    }
}

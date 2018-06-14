using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DController : PlayerMoveController
{
    public override void PositionInitialization()
    {
        playerposition = new Vector3(0, 1, -4.5f);
    }

    public override void Movement()
    {
        var speed = 3.0f;
        var h = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        var AxisInput = new Vector3(h, 0.0f);
        transform.position += AxisInput;
        var MoveInput = AxisInput.magnitude >= 0.1f;
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
        //BFFALO　3番キーを押したら
        if (Input.GetButtonDown("JoyStick2") && !Jump)
        {
            rig.AddForce(Vector2.up * flap);
            Jump = true;
        }
        //BFFALO　２番キーを押したら
        if (Input.GetButtonDown("JoyStick1"))
        {
            Manager.PlayerMoveChange<Player3DController>();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DController : PlayerMoveController
{
    public override void PositionInitialization()
    {
        Vector3 pos = transform.localPosition;

        if (Manager.IsRight) pos.x = -10;
        else pos.x = 10;

        transform.localPosition = pos;
    }

    public override void Movement()
    {
        if (!Manager.IsRight)
        {
            var speed = 3.0f;
            var h = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            var AxisInput = new Vector3(0, 0.0f, h);
            transform.position += AxisInput;
            var MoveInput = AxisInput.magnitude <= 0.1f;
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
        }
        else
        {
            var speed = 3.0f;
            var h = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            var AxisInput = new Vector3(0, 0.0f, h);
            transform.position -= AxisInput;
            var MoveInput = AxisInput.magnitude <= 0.1f;
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
        }


        //BFFALO　3番キーを押したら
        if (Input.GetButtonDown("JoyStick2") && !Jump)
        {
            rig.AddForce(Vector2.up * flap);
            anim.SetBool("Jump", true);
            anim.SetBool("walk", false);
            Jump = true;
        }
        //BFFALO　２番キーを押したら
        if (Input.GetButtonDown("JoyStick1"))
        {
            Vector3 pos = transform.localPosition;
            pos.x = 0;
            transform.localPosition = pos;

            Manager.GController.ChangeDimension();
        }
    }
}

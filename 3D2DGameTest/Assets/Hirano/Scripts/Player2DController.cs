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
    }
}

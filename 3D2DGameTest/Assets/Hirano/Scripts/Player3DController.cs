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
        var h = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        var v = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        var AxisInput = new Vector3(h, 0.0f, v);
        transform.position += AxisInput;
        var degree = Mathf.Atan2(AxisInput.x, AxisInput.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.up * degree);
    }
}

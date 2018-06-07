using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed = 150.0f;
    float DistanceToPlayer = 2.0f;
    float SlideDistans = 0.0f;
    float Heght = 1.2f;
	// Use this for initialization
	void Start ()
    {

	}

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveCamera()
    {
        var rotX = Input.GetAxis("Horizontal2") * Time.deltaTime * speed;
        var rotY = Input.GetAxis("Vertical2") * Time.deltaTime * speed;

        var lookAt = target.position + Vector3.up * Heght;
        //回転
        transform.RotateAround(lookAt, Vector3.up, rotX);
        transform.RotateAround(lookAt, Vector3.right, rotY);
        //カメラがプレイヤーの真下や真上にある時それ以上回転させないようにする
        if (transform.forward.y > 0.5f && rotY > 0.5f)
        {
            rotY = 0;
        }
        if (transform.forward.y < -0.5f && rotY > -0.5f)
        {
            rotY = 0;
        }
        //カメラとプレイヤーの距離を調整
        transform.position = lookAt - transform.forward * DistanceToPlayer;
        //注視点の設定
        transform.LookAt(lookAt);
        //カメラを横にずらして中央を開ける
        transform.position = transform.position + transform.right * SlideDistans;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    GameObject targetObj;
    Vector3 targetPos;
    
    // Use this for initialization
    void Start ()
    {
        targetObj = GameObject.Find("TargetGameObject");
        targetPos = targetObj.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (TestMove._Moveflg)
        {
            // targetの移動量分、自分（カメラ）も移動する
            transform.position += targetObj.transform.position - targetPos;
            targetPos = targetObj.transform.position;
            // マウスの移動量
            float mouseInputX = Input.GetAxis("Horizontal2");
            // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 200f);
        }
    }
}

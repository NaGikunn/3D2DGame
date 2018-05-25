using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TochTest : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
       
	}

    //クリックしたオブジェクトの取得
    public void ObjectClick()
    {
        GameObject TransportObj = null;
        Rigidbody ObjectRig;
        float power = 10.0f;
        if (Input.GetButton("JoyStick L1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(ray,out hit))
            {
                TransportObj = hit.collider.gameObject;
                ObjectRig = TransportObj.GetComponent<Rigidbody>();
                //nullチェック
                if (ObjectRig != null)
                {
                    //タッチしたとき力を加える
                    ObjectRig.AddForce(Vector3.up * power);
                    //オブジェクトの位置をスクリーン座標に変換
                    Vector3 ObjVec = Camera.main.WorldToScreenPoint(TransportObj.transform.position);
                    //マウスの場所とオブジェクトの位置を統合
                    Vector3 mousePointInScren = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ObjVec.z);
                    //マウスの位置をワールド座標に変換
                    Vector3 mousePointInWorld = Camera.main.ScreenToWorldPoint(mousePointInScren);
                    //ｚ軸は反映されない
                    mousePointInWorld.z = TransportObj.transform.position.z;
                    //保存したマウスのワールド座標をタッチしたオブジェクトに反映
                    TransportObj.transform.position = mousePointInWorld;
                    ObjectRig.AddForce(mousePointInWorld);
                }
            }
        }
    }
}

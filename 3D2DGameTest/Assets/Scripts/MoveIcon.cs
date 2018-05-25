using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIcon : MonoBehaviour
{
    //アイコンが1秒間に何ピクセル移動するか
    [SerializeField]
    private float iconspeed = Screen.width;
    //アイコンのサイズ取得で使用
    private RectTransform rect;
    //アイコンが画面内に収まるためのオフセット値
    private Vector2 offset;

	// Use this for initialization
	void Start ()
    {
        rect = GetComponent<RectTransform>();
        //オフセット値をアイコンのサイズの半分で設定
        offset = new Vector2(rect.sizeDelta.x / 2f, rect.sizeDelta.y / 2f);
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //移動キーを押していなければ何もしない
        if (Input.GetAxis("Horizontal2") == 0f && Input.GetAxis("Vertical2") == 0f)
        {
            return;
        }
        //移動先を計算
        var pos = rect.anchoredPosition + new Vector2(Input.GetAxis("Horizontal2") * iconspeed, Input.GetAxis("Vertical2") * iconspeed) * Time.deltaTime;
        //アイコンが画面外に出ないようにする
        pos.x = Mathf.Clamp(pos.x, -Screen.width * 0.5f + offset.x, Screen.width * 0.5f - offset.x);
        pos.y = Mathf.Clamp(pos.y, -Screen.height * 0.5f + offset.y, Screen.height * 0.5f - offset.y);
        //アイコンの位置を設定
        rect.anchoredPosition = pos;

        IconObjectClick();
    }

    //クリックしたオブジェクトの取得
    public void IconObjectClick()
    {
        GameObject TransportObj = null;
        Rigidbody ObjectRig;
        float power = 10.0f;
        if (Input.GetButton("JoyStick L1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(transform.position);
            RaycastHit hit = new RaycastHit();
            Debug.DrawRay(ray.origin,ray.direction * 100,Color.blue,3,false);
            if (Physics.Raycast(ray, out hit))
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
                    //UIの場所とオブジェクトの位置を統合
                    Vector3 UIPointInScren = new Vector3(transform.position.x,transform.position.y, ObjVec.z);
                    //UIの位置をワールド座標に変換
                    Vector3 UIPointInWorld = Camera.main.ScreenToWorldPoint(UIPointInScren);
                    //ｚ軸は反映されない
                    UIPointInWorld.z = TransportObj.transform.position.z;
                    //保存したマウスのワールド座標をタッチしたオブジェクトに反映
                    TransportObj.transform.position = UIPointInWorld;
                }
            }
        }
    }
}

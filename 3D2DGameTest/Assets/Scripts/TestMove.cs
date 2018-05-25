using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMove : MonoBehaviour
{
    [SerializeField]
    private GameObject Icon;
    [SerializeField]
    private Text Modelabel;
    public static bool _Moveflg = true;
    float inputHorizontal;
    float inputVertical;
    Rigidbody rb;
    float moveSpeed = 3f;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        //BFFALO　２番キーを押したら
        if (Input.GetButtonDown("JoyStick1"))
        {
            _Moveflg = false;
            rb.isKinematic = true;
            Icon.SetActive(true);
            //UIを変更
            Modelabel.text = ("Mode:Transport");
        }
        //BAFFALO 3番キーを押したら
        if (Input.GetButtonDown("JoyStick2"))
        {
            rb.isKinematic = false;
            _Moveflg = true;
            Icon.SetActive(false);
            Modelabel.text = ("Mode:Move");
        }
    }

    void FixedUpdate()
    {
        if (_Moveflg)
        {
            // カメラの方向から、X-Z平面の単位ベクトルを取得
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            // 方向キーの入力値とカメラの向きから、移動方向を決定
            Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;
            // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
            rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);
            // キャラクターの向きを進行方向に
            if (moveForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }
        }
    }
}

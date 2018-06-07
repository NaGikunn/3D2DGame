using UnityEngine;
using System.Collections;

public class Players : MonoBehaviour
{
    // 移動スピード
    public Vector2 speed = new Vector2(0.05f, 0.05f);

    //public GameObject GameOverImage;

    ////Animatorコンポーネントへの参照
    //Animator animator;

    void Start()
    {
        ////GameOverImage.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
        //GameOverImage.SetActive(false);
        ////Animatorコンポーネントを取得してキャッシュする
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        //現在位置をposに代入
        Vector2 pos = transform.position;
        //float speed = 1.0f;
        //左キーを押し続けていたら
        if (Input.GetKey("left"))
        {
            //代入したposに対して加算減算を行う
            pos.x -= speed.x;
        }
        //右キーを押し続けていたら
        if (Input.GetKey("right"))
        {
            //代入したposに対して加算減算を行う
            pos.x += speed.x;
        }
        //現在の位置に加算減算を行ったposを代入する
        transform.position = pos;

        // 右・左
        //float x = Input.GetAxisRaw("Horizontal");

        // 上・下
        //float y = Input.GetAxisRaw("Vertical");

        // 移動する向きを求める
        //Vector2 direction = new Vector2(x, y).normalized;

        // 移動する向きとスピードを代入する
        //GetComponent<Rigidbody2D>().velocity = direction * speed;

        //animator.SetTrigger("Jump");
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        Destroy(this.gameObject);
    //        //GameOverImage.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
    //        GameOverImage.SetActive(true);
    //    }
    //}
}
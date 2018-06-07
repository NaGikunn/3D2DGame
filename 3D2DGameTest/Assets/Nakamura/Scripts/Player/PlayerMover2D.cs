using UnityEngine;

namespace Dimension.Player
{
    public class PlayerMover2D : PlayerMover
    {
        //-----------------------------------------------------
        //  初期化
        //-----------------------------------------------------
        public override void Initialize()
        {
            transformCache.position = new Vector3(0, 1, -4.5f);

            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Collider>());
            gameObject.AddComponent<CapsuleCollider2D>();
            gameObject.AddComponent<Rigidbody2D>();
        }
        //-----------------------------------------------------
        //  行動
        //-----------------------------------------------------
        public override void Mover()
        {

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transformCache.position += Camera.main.transform.right * 3.0f * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transformCache.position += -Camera.main.transform.right * 3.0f * Time.deltaTime;
            }
        }
    }
}
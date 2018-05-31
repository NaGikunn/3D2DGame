using UnityEngine;

namespace Dimension.Player
{
    public class PlayerMover3D : PlayerMover
    {
        //-----------------------------------------------------
        //  初期化
        //-----------------------------------------------------
        public override void Initialize()
        {
            transformCache.position = new Vector3(0, 2, 0);

            gameObject.AddComponent(typeof(Rigidbody));
        }
        //-----------------------------------------------------
        //  行動
        //-----------------------------------------------------
        public override void Mover()
        {
            if (Input.GetKey(KeyCode.UpArrow)) {
                transformCache.position += transformCache.forward * 3.0f * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.DownArrow)) {
                transformCache.position += -transformCache.forward * 3.0f * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                transformCache.position += transformCache.right * 3.0f * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.LeftArrow)) {
                transformCache.position += -transformCache.right * 3.0f * Time.deltaTime;
            }
        }
    }
}
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
            //rigidbodyCache.useGravity = true;

            Vector3 pos = transformCache.localPosition;

            if (PController.IsRight) pos.x = -10;
            else                     pos.x =  10;

            transformCache.localPosition = pos;
        }
        //-----------------------------------------------------
        //  行動
        //-----------------------------------------------------
        public override void Move(KeyState key)
        {
            // 移動
            if (key.Axis.x > 0.5f)  {
                transformCache.position +=  Camera.main.transform.right * 3.0f * Time.deltaTime;
            } else 
            if (key.Axis.x < -0.5f) {
                transformCache.position += -Camera.main.transform.right * 3.0f * Time.deltaTime;
            }

            // ジャンプ
            if (PController.IsGround && key.Jump) {
                rigidbodyCache.AddForce(Vector3.up * 250);
            }
            
            // モード切替
            if(key.Action) {
                Vector3 pos = transformCache.position;
                pos.x = 0;
                transformCache.localPosition = pos;

                PController.GController.ChangeDimension();
            }
        }
    }
}
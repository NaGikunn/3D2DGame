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
            //rigidbodyCache.useGravity = true;
        }
        //-----------------------------------------------------
        //  行動
        //-----------------------------------------------------
        public override void Move(KeyState key)
        {
            // 移動方向
            Vector3 moveVec = new Vector3(-key.Axis.x, 0, -key.Axis.y);

            // ジャンプ
            if (PController.IsGround  && key.Jump) {
                rigidbodyCache.AddForce(Vector3.up * 250);
            }

            // モード切替
            if (key.Action) {
                PController.GController.ChangeDimension();
            }

            // 更新
            transformCache.localPosition += moveVec * 3.0f * Time.deltaTime;
        }
    }
}
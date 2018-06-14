using UnityEngine;

namespace Dimension.Camera2D3D
{
    public class CameraWork2D : CameraWork
    {
        const float MOVE_HEIGHT_MIN =  4.0f;
        const float MOVE_HEIGHT_MAX =  5.0f;
        const float MOVE_WIDTH_MIN  = -32.0f;
        const float MOVE_WIDTH_MAX  = -4.0f;
        //---------------------------------------------------------------
        //  プロパティ
        //---------------------------------------------------------------
        
        //---------------------------------------------------------------
        //  初期化
        //---------------------------------------------------------------
        public override void Initialize()
        {
            MyCamera.orthographic = true;
        }
        //---------------------------------------------------------------
        //  行動
        //---------------------------------------------------------------
        public override void Move()
        {
            if (Target == null) return;

            transformCache.localPosition = new Vector3(
                transformCache.localPosition.x,
                Mathf.Clamp(Target.transform.position.y, MOVE_HEIGHT_MIN, MOVE_HEIGHT_MAX),
                Mathf.Clamp(Target.transform.position.z, MOVE_WIDTH_MIN, MOVE_WIDTH_MAX)
                );
        }
    }
}
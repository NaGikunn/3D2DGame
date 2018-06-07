using UnityEngine;

namespace Dimension.Camera2D3D
{
    public class CameraWork3D : CameraWork
    {
        //-----------------------------------------------------
        //  初期化
        //-----------------------------------------------------
        public override void Initialize()
        {
            MyCamera.orthographic = false;
            MyCamera.cullingMask |= 1 << LayerMask.NameToLayer("GlassBox");

            transformCache.position = new Vector3(0, 5, -10);
            transformCache.eulerAngles = new Vector3(20, 0, 0);
        }
        //-------------------------------------------------
        //  行動
        //-------------------------------------------------
        public override void Move()
        {
            TrackingTarget();
        }
        //-------------------------------------------------
        //  追跡
        //-------------------------------------------------
        Vector3 targetRange = new Vector3(0, 5.0f, -5.0f);
        void TrackingTarget()
        {
            transformCache.position = CameraTarget.position + targetRange;
        }
        [ContextMenu("Change")]
        void ChangeCamera()
        {
            
        }
    }
}
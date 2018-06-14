using UnityEngine;
using System.Collections;

namespace Dimension.Camera2D3D
{
    public class CameraChangeWork : CameraWork
    {
        const float FIELD_VIEW_MIN = 10.0f; // 最小画角
        const float FIELD_VIEW_MAX = 60.0f; // 最大画角
        const float BACK_LENGTH    = 41.0f; // ドリーアウト距離

        public Vector3 fromPoint;  // 元の位置
        public Vector3 nextPoint;  // 次の位置
        Vector3 fromDir;    // 元の角度
        Vector3 nextDir;    // 次の角度

        float fromView;     // 元の画角
        float nextView;     // 次の画角
        float changeValue;    // 変化値

        //-----------------------------------------------------
        //  初期化
        //-----------------------------------------------------
        public override void Initialize()
        {
            fromPoint = transformCache.localPosition;
            nextPoint = Target.transform.localPosition;
            fromDir   = transformCache.localEulerAngles;
            changeValue = 0;

            if(GameMode == Mode.Third)
            {   // 3D → 2D
                fromView = FIELD_VIEW_MAX;      // 元の画角に最大画角を設定
                nextView = FIELD_VIEW_MIN;      // 次の画角に最小画角を設定
                // 次の角度
                nextDir = (Target.IsRight) ? Vector3.right : Vector3.left;
                // 次の位置
                nextPoint += -nextDir * BACK_LENGTH;

            } else
            {   // 2D → 3D
                MyCamera.orthographic = false;  // Cameraを透視投影に変換
                fromView = FIELD_VIEW_MIN;      // 元の画角に最小画角を設定
                nextView = FIELD_VIEW_MAX;      // 次の画角に最大画角を設定
                // 次の角度
                nextDir = new Vector3(3, -5, -5);
                // 次の位置
                nextPoint.x = 0;
                nextPoint += -nextDir;
            }
        }
        //-----------------------------------------------------
        //  行動
        //-----------------------------------------------------
        public override void Move()
        {
            if (IsFinish()) return;

            changeValue = Mathf.Min(changeValue + Time.deltaTime * 2, 1);

            MyCamera.fieldOfView            = Mathf.Lerp(fromView, nextView, changeValue);
            transformCache.localPosition    = Vector3.Lerp(fromPoint, nextPoint, changeValue);

            transformCache.LookAt(Target.transform.position);
            //transformCache.forward          = Vector3.Lerp(fromDir, nextDir, changeValue);

            if(IsFinish())
            {
                FinishWork();
            }
        }
        //-----------------------------------------------------
        //  終了判断
        //-----------------------------------------------------
        bool IsFinish()
        {
            return changeValue >= 1;
        }
        //-----------------------------------------------------
        //  終了処理
        //-----------------------------------------------------
        void FinishWork()
        {
            if(GameMode == Mode.Third)
            {
                MyCamera.orthographic = true;
                CController.GController.ChangeMode2D();
                return;
            }

            CController.GController.ChangeMode3D();
        }
    }
}
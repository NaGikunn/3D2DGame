using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dimension.Stage
{
    public class StageObject2D : MonoBehaviour
    {
        Vector3 cameraAxis; //カメラの軸の向き
        Transform transformCache;
        //-----------------------------------------------------
        //  2DMode時のColliderの状態
        //-----------------------------------------------------
        //[ContextMenu("ColliderState2D")]
        //protected override void ColliderState2D()
        //{
        //    ObjectCollider.enabled = true;

        //    SizeAdjustCollider();
        //    //PositioningCollider();
        //}
        //-----------------------------------------------------
        //  3DMode時のColliderの状態
        //-----------------------------------------------------
        //protected override void ColliderState3D()
        //{
        //    ObjectCollider.enabled = false;
        //}
        //-----------------------------------------------------
        //  Colliderのサイズを調整
        //-----------------------------------------------------
        void SizeAdjustCollider()
        {
            //const float DEFAULT_SIZE = 1.0f;
            Vector3 cAxis = GetObjectAxis(Camera.main.transform.localEulerAngles.y);

            //ObjectCollider.size = new Vector3(
            //    (Mathf.FloorToInt(cAxis.x) != 0) ? DEFAULT_SIZE / transformCache.localScale.x : DEFAULT_SIZE,
            //    (Mathf.FloorToInt(cAxis.y) != 0) ? DEFAULT_SIZE / transformCache.localScale.y : DEFAULT_SIZE,
            //    (Mathf.FloorToInt(cAxis.z) != 0) ? DEFAULT_SIZE / transformCache.localScale.z : DEFAULT_SIZE
            //    );
            
        }
        //-----------------------------------------------------
        //  Colliderの位置を調整
        //-----------------------------------------------------
        void PositioningCollider()
        {
            Vector3 cAxis = GetObjectAxis(Camera.main.transform.eulerAngles.y);
            Vector3 dest = GetDestination(cAxis);
            //ObjectCollider.center = WorldToColliderCenter(dest);
        }
        //-----------------------------------------------------
        //  Objectの向いている軸
        //-----------------------------------------------------
        Vector3 GetObjectAxis(float degree)
        {
            const int FORWARD = 0, RIGHT = 1, BACK = 2, LEFT = 3;
            switch (Mathf.FloorToInt(degree) / 90)
            {
                case FORWARD: return Vector3.forward;
                case RIGHT:   return Vector3.right;
                case BACK:    return Vector3.back;
                case LEFT:    return Vector3.left;
                default:      return Vector3.zero;
            }
        }
        //-----------------------------------------------------
        //  目的地を求める(ワールド座標)
        //-----------------------------------------------------
        Vector3 GetDestination(Vector3 cAxis)
        {
            const float STAGE_LENGTH = 10;
            return new Vector3(
                (cAxis.x != 0) ? ((STAGE_LENGTH - 1) * 0.5f) * cAxis.x : transformCache.position.x,
                (cAxis.y != 0) ? ((STAGE_LENGTH - 1) * 0.5f) * cAxis.y : transformCache.position.y,
                (cAxis.z != 0) ? ((STAGE_LENGTH - 1) * 0.5f) * cAxis.z : transformCache.position.z);
        }
        //-----------------------------------------------------
        //  Collider.centerをワールド座標の位置に移動させる
        //-----------------------------------------------------
        Vector3 WorldToColliderCenter(Vector3 destination)
        {
            // 移動量
            float moveNum = (destination - transformCache.position).magnitude;
            Debug.Log(this.name + ":" + moveNum);
            
            // 移動する軸
            

            //float centerX = 
            // Collider.size = (目的地 - 現在地) * カメラ軸の絶対値 / スケール * オブジェクトの軸の向き
            return new Vector3(
                (transformCache.right * moveNum).magnitude,
                0,
                (destination.z - transformCache.position.z) / transformCache.localScale.z);
        }

        [ContextMenu("Axis")]
        void LocalAxis()
        {
            Debug.Log("RIGHT" + transformCache.right);
            Debug.Log("FORWARD" + transformCache.forward);
        }
    }
}
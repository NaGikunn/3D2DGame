using UnityEngine;

namespace Dimension.Stage
{
    public class StageObject : MonoBehaviour
    {
        protected Transform transformCache;

        [SerializeField] Vector2[] collider2DVertex_AxisX;
        [SerializeField] Vector2[] collider2DVertex_AxisZ;

        //-----------------------------------------------------
        //  プロパティ
        //-----------------------------------------------------

        //=====================================================
        void Awake()
        {
            transformCache = transform;
            
            //GameObject.Find("Stage").GetComponent<StageController>().AddStageObject(this);
        }
        //-----------------------------------------------------
        //  Gizmo
        //-----------------------------------------------------
    }
}
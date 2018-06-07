using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dimension.Stage
{
    public class StageController : MonoBehaviour
    {
        // StageObjectの親
        [SerializeField, Tooltip("StageObjectの親")]
        Transform objParent;

        // StageObjectリスト
        //public StageObject<Collider>[] objectList;
        List<StageObject> objectList = new List<StageObject>();

        //=====================================================
        void Start()
        {
            
        }
        void FixedUpdate()
        {

        }
        //-----------------------------------------------------
        //  ステージオブジェクトの追加
        //-----------------------------------------------------
        public void AddStageObject(StageObject obj)
        {
            objectList.Add(obj);
        }
        //-----------------------------------------------------
        //  StageObjectの状態変更
        //-----------------------------------------------------
        public void ChangeStageMode(Mode mode)
        {
            foreach(StageObject obj in objectList)
            {
                //obj.ChangeCollider(mode);
            }
        }
    }
}
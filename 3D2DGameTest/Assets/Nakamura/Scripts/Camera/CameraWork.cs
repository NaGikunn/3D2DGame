using UnityEngine;

namespace Dimension.Camera2D3D
{
    public abstract class CameraWork : MonoBehaviour
    {
        protected Transform transformCache;
        protected Camera _myCamera;

        //-----------------------------------------------------
        //  プロパティ
        //-----------------------------------------------------
        public Transform CameraTarget { get; set; }
        protected Camera MyCamera
        {
            get { return _myCamera; }
            private set { _myCamera = value; }
        }
        //=====================================================
        void Awake()
        {
            transformCache = transform;
            MyCamera = GetComponent<Camera>();
        }
        //-----------------------------------------------------
        //  抽象メソッド
        //-----------------------------------------------------
        public abstract void Initialize();  // 初期化
        public abstract void Move();        // 行動
    }
}
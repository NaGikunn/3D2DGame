using UnityEngine;
using Dimension.Player;

namespace Dimension.Camera2D3D
{
    public abstract class CameraWork : MonoBehaviour
    {
        protected Transform transformCache;

        //-----------------------------------------------------
        //  プロパティ
        //-----------------------------------------------------
        protected CameraController CController { get; private set; }
        protected Mode GameMode { get { return CController.GetGameMode(); } }
        protected Camera MyCamera   { get; private set; }
        protected PlayerManagerController Target { get; private set; }
        //=====================================================
        void Awake()
        {
            transformCache  = transform;
            CController     = GetComponent<CameraController>();
            MyCamera        = GetComponent<Camera>();
        }
        //-----------------------------------------------------
        //  Targetを設定
        //-----------------------------------------------------
        public void SetTarget(PlayerManagerController target)
        {
            if (Target == null) Target = target;
        }
        //-----------------------------------------------------
        //  抽象メソッド
        //-----------------------------------------------------
        public abstract void Initialize();  // 初期化
        public abstract void Move();        // 行動
    }
}
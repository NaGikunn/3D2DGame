using UnityEngine;

namespace Dimension.Player
{
    public abstract class PlayerMover : MonoBehaviour
    {
        protected Transform transformCache;
        protected Rigidbody rigidbodyCache;

        //-----------------------------------------------------
        //  プロパティ
        //-----------------------------------------------------
        protected TestPlayer PController { get; private set; }
        protected Mode GameMode { get { return PController.GController.GameMode; } }
        //=====================================================
        void Awake()
        {
            transformCache  = transform;
            rigidbodyCache  = GetComponent<Rigidbody>();
            PController     = GetComponent<TestPlayer>();
            Initialize();
        }
        //-----------------------------------------------------
        //  抽象メソッド
        //-----------------------------------------------------
        public abstract void Initialize();      // 初期化
        public abstract void Move(KeyState key);// 行動
    }
}
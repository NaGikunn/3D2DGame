using UnityEngine;

namespace Dimension.Player
{
    public abstract class PlayerMover : MonoBehaviour
    {
        protected Transform transformCache;

        //=====================================================
        void Awake()
        {
            transformCache = transform;
            Initialize();
        }
        //-----------------------------------------------------
        //  抽象メソッド
        //-----------------------------------------------------
        public abstract void Initialize();  // 初期化
        public abstract void Mover();       // 行動
    }
}
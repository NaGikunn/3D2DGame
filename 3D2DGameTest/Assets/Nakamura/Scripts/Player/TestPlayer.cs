using UnityEngine;

namespace Dimension.Player
{
    public struct KeyState
    {
        public bool Action;
        public bool Jump;

        public Vector2 Axis;
    }

    public class PlayerManagerController : MonoBehaviour
    {
        Transform transformCache;
        Rigidbody rigidbodyCache;

        PlayerMover pMover;

        //-----------------------------------------------------
        //  プロパティ
        //-----------------------------------------------------
        // Transform
        public Vector3 Position { get { return transformCache.position; } }
        public Vector3 LocalPosition { get { return transformCache.localPosition; } }
        public Vector3 Forward { get { return transformCache.forward; } }
        // ステータス
        public bool IsStop { get; set; }
        public bool IsGround { get { return Mathf.Abs(rigidbodyCache.velocity.y) <= 0.01f; } }
        public bool IsRight { get { return transformCache.localPosition.x <= 0; } }   // ステージ中央より右にいるか

        public GameController GController { get; private set; }

        //=====================================================
        void Start()
        {
            transformCache = transform;
            rigidbodyCache = GetComponent<Rigidbody>();
            IsStop = false;
            if (pMover == null) ChangeMover<PlayerMover3D>();
        }
        void Update()
        {
            if (IsStop) return;
            pMover.Move(InputKey());
        }
        //-----------------------------------------------------
        //  ゲームコントローラー受け取り
        //-----------------------------------------------------
        public void SetGameController(GameController gCon)
        {
            if (GController == null) GController = gCon;
        }
        //-----------------------------------------------------
        //  操作の変更
        //-----------------------------------------------------
        public void ChangeMover<PM>() where PM : PlayerMover
        {
            Destroy(pMover);
            pMover = gameObject.AddComponent<PM>();
        }
        //-----------------------------------------------------
        // 入力
        //-----------------------------------------------------
        KeyState InputKey()
        {
            KeyState key = new KeyState();

            if (Input.GetJoystickNames().Length == 0)
            {   // キーボード
                key.Action = Input.GetKeyDown(KeyCode.Z);
                key.Jump = Input.GetKeyDown(KeyCode.Space);

                key.Axis.x += (Input.GetKey(KeyCode.RightArrow)) ? 1 : 0;
                key.Axis.x -= (Input.GetKey(KeyCode.LeftArrow)) ? 1 : 0;
                key.Axis.y += (Input.GetKey(KeyCode.UpArrow)) ? 1 : 0;
                key.Axis.y -= (Input.GetKey(KeyCode.DownArrow)) ? 1 : 0;
                key.Axis = key.Axis.normalized;
            }
            else
            {   // ゲームパッド
                //BFFALO　２番キーを押したら
                key.Action = Input.GetButtonDown("JoyStick1");
            }

            return key;
        }
    }
}
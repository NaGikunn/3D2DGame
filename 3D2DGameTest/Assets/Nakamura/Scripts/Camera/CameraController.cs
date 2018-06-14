using System.Collections;
using UnityEngine;
using Dimension.Player;
using Dimension.Camera2D3D.Effect;

namespace Dimension.Camera2D3D
{
    public class CameraController : MonoBehaviour
    {
        GameController gController;

        public PlayerManagerController player;
        CameraWork cWork;

        //-----------------------------------------------------
        //  プロパティ
        //-----------------------------------------------------
        public GameController GController { get; private set; }
        public bool IsStop { get; set; }
        //=====================================================
        void Start()
        {
            IsStop = false;
            if (cWork == null) ChangeWork<CameraWork3D>();
        }
        void LateUpdate()
        {
            if (IsStop) return;
            cWork.Move();
        }
        //-----------------------------------------------------
        //  カメラワークを変更
        //-----------------------------------------------------
        public void ChangeWork<CW>() where CW : CameraWork
        {
            Destroy(cWork);
            cWork = gameObject.AddComponent<CW>();
            cWork.SetTarget(player);
            cWork.Initialize();
        }

        public void Change()
        {
            ChangeWork<CameraWorkNull>();
        }
        //-----------------------------------------------------
        //  ゲームコントローラー受け取り
        //-----------------------------------------------------
        public void SetGameController(GameController gCon)
        {
            if (GController == null) GController = gCon;
        }
        //-----------------------------------------------------
        //  ゲームモード取得
        //-----------------------------------------------------
        public Mode GetGameMode()
        {
            return GController.GameMode;
        }
    }
}
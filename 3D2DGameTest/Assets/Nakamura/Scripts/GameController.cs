using System.Collections;
using UnityEngine;
using Dimension.Camera2D3D;
using Dimension.Player;
using Dimension.Stage;

namespace Dimension
{
    public enum Mode
    {
        Second,
        Third
    }
    public class GameController : MonoBehaviour
    {
        public CameraController cController;
        public TestPlayer       pController;

        //-----------------------------------------------------
        //  プロパティ
        //-----------------------------------------------------
        public Mode GameMode { get; private set; }
        //=====================================================
        void Start()
        {
            cController.SetGameController(this);
            pController.SetGameController(this);

            ChangeMode3D();
        }
        //-----------------------------------------------------
        //  3D2Dの切り替え
        //-----------------------------------------------------
        public void ChangeDimension()
        {
            ChangeMode<PlayerMoverNull, CameraChangeWork>();
        }
        //-----------------------------------------------------
        //  3DModeへ切り替え
        //-----------------------------------------------------
        public void ChangeMode3D()
        {
            ChangeMode<PlayerMover3D, CameraWork3D>();
            GameMode = Mode.Third;
        }
        //-----------------------------------------------------
        //  2DModeへ切り替え
        //-----------------------------------------------------
        public void ChangeMode2D()
        {
            ChangeMode<PlayerMover2D, CameraWork2D>();
            GameMode = Mode.Second;
        }
        //-----------------------------------------------------
        //  Modeの変更
        //-----------------------------------------------------
        void ChangeMode<PM, CW>() where PM : PlayerMover where CW : CameraWork
        {
            pController.ChangeMover<PM>();
            cController.ChangeWork<CW>();
        }
    }
}
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
        public StageController stageController;
        public CameraController cameraController;
        public TestPlayer player;

        delegate void Switch2DTo3D();
        delegate void Switch3DTo2D();

        bool isRun = false;

        //=====================================================
        void Start()
        {
            ChangeMode<PlayerMover3D, CameraWork3D>();
            DuringFadeEnabled(true);
        }
        void OnGUI()
        {
            if (isRun) return;

            if (GUI.Button(new Rect(300, 10, 100, 30), "2DMode"))
            {
                StartCoroutine(ChangeModeCoroutine(Mode.Second));
            }
            if (GUI.Button(new Rect(300, 50, 100, 30), "3DMode"))
            {
                StartCoroutine(ChangeModeCoroutine(Mode.Third));
            }
        }
        //-----------------------------------------------------
        //  Modeの変更コルーチン
        //-----------------------------------------------------
        IEnumerator ChangeModeCoroutine(Mode mode)
        {
            DuringFadeEnabled(false);

            yield return StartCoroutine(cameraController.FadeOutCoroutine());

            switch (mode) {
                case Mode.Second: ChangeMode<PlayerMover2D, CameraWork2D>(); break;
                case Mode.Third:  ChangeMode<PlayerMover3D, CameraWork3D>(); break;
            }
            stageController.ChangeStageMode(mode);

            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(cameraController.FadeInCoroutine());

            DuringFadeEnabled(true);
        }
        //-----------------------------------------------------
        //  Modeの変更
        //-----------------------------------------------------
        void ChangeMode<PM, CW>() where PM : PlayerMover where CW : CameraWork
        {
            player.ChangeMover<PM>();
            cameraController.ChangeWork<CW>();
        }
        //-----------------------------------------------------
        //  フェード中の処理の有無
        //-----------------------------------------------------
        void DuringFadeEnabled(bool flg)
        {
            isRun = player.IsStop = !flg;
        }
    }
}
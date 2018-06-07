using UnityEngine;
using Dimension.Stage;

namespace Dimension.Camera2D3D
{
    public class CameraWork2D : CameraWork
    {
        const float PI = Mathf.PI;  // 円周率
        const int RIGHT = 1;       // 右
        const int LEFT = -1;       // 左

        [SerializeField] Transform stageCenter;
        [SerializeField] StageController stage;

        float _pos = PI;

        // 回転用
        bool isRotation = false;
        int rotationDir;
        int timer;

        enum Behaviour
        {
            Left = -1,
            None = 0,
            Right = 1
        }

        //-------------------------------------------------------------------------
        //  プロパティ
        //-------------------------------------------------------------------------
        float Pos
        {
            get { return _pos; }
            set
            {
                _pos = Mathf.Repeat(value, 2 * PI);
                UpdateSatelite();
            }
        }
        //=========================================================================
        void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 100, 30), "RIGHT") && !isRotation)
            {
                StartRotation(RIGHT);
            }
            if (GUI.Button(new Rect(10, 50, 100, 30), "LEFT") && !isRotation)
            {
                StartRotation(LEFT);
            }
        }
        //-----------------------------------------------------
        //  初期化
        //-----------------------------------------------------
        public override void Initialize()
        {
            MyCamera.orthographic = true;
            MyCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("GlassBox"));

            

            stageCenter = GameObject.Find("StageCube").transform;
            UpdateSatelite();

            Debug.Log("Camera2D");
        }
        //-----------------------------------------------------
        //  行動
        //-----------------------------------------------------
        public override void Move()
        {
            RotationMove();
            UpdateSatelite();
        }
        //-----------------------------------------------------
        // 位置と向きを更新
        //-----------------------------------------------------
        void UpdateSatelite()
        {
            UpdatePosition();   // 位置を更新
            UpdateDirection();  // 向きを更新
        }
        //-----------------------------------------------------
        // 回転開始
        //-----------------------------------------------------
        void StartRotation(int dir)
        {
            timer = 0;
            rotationDir = dir;
            isRotation = true;
        }
        //-----------------------------------------------------
        // 回転移動
        //-----------------------------------------------------
        void RotationMove()
        {
            if (!isRotation) return;

            timer++;
            Pos += rotationDir * 0.5f * Time.deltaTime * PI;

            if (timer >= 50)
            {
                isRotation = false;
                if (stage == null) stage = GameObject.Find("Stage").GetComponent<StageController>();
                stage.ChangeStageMode(Mode.Second);
            }
        }
        //-----------------------------------------------------
        // 位置を更新
        //-----------------------------------------------------
        void UpdatePosition()
        {
            const float STAGE_FROM_RANGE = 10.0f;

            Vector3 movePos = transformCache.position;
            movePos.x = stageCenter.position.x + STAGE_FROM_RANGE * Mathf.Sin(Pos);
            movePos.z = stageCenter.position.z + STAGE_FROM_RANGE * Mathf.Cos(Pos);
            movePos.y = 5.0f;
            transformCache.position = movePos;
        }
        //-----------------------------------------------------
        // 向きを更新
        //-----------------------------------------------------
        void UpdateDirection()
        {
            Vector3 dir = new Vector3(0, 180 + 180 * Pos / PI, 0);
            transformCache.eulerAngles = dir;
        }
        //-----------------------------------------------------
        //  回転するかどうか
        //-----------------------------------------------------
        int RotationDirection()
        {
            float posX = Camera.main.WorldToViewportPoint(CameraTarget.position).x;
            if (posX < 0.1f) return LEFT;
            if (posX > 0.9f) return RIGHT;
            return 0;
        }
    }
}
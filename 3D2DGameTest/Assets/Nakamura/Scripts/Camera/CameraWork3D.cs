using UnityEngine;

namespace Dimension.Camera2D3D
{
    public class CameraWork3D : CameraWork
    {

        Timer lerpTimer;
        bool  beforeFlg;

        //-----------------------------------------------------
        //  初期化
        //-----------------------------------------------------
        public override void Initialize()
        {
            MyCamera.orthographic = false;
            lerpTimer = new Timer();
            beforeFlg = Target.IsRight;
        }
        //-------------------------------------------------
        //  行動
        //-------------------------------------------------
        Vector3 RightVec = new Vector3(-3, 5, 5);
        float RightAngle = 150.0f;
        Vector3 LeftVec = new Vector3(3, 5, 5);
        float LeftAngle = 210.0f;

        public override void Move()
        {
            bool isRight = Target.IsRight;
            if (isRight != beforeFlg) {
                lerpTimer.Clear();
            }

            lerpTimer.Counter(Time.deltaTime * 2);

            Vector3 posVec = (isRight) ? RightVec : LeftVec;
            float angle = (isRight) ? RightAngle : LeftAngle;

            transformCache.position = Vector3.Lerp(transformCache.position, Target.transform.position + posVec, lerpTimer.Time);
            transformCache.eulerAngles = Vector3.Lerp(transformCache.eulerAngles, new Vector3(20, angle, 0), lerpTimer.Time);

            beforeFlg = isRight;
        }
    }

    public class Timer
    {
        float _time;
        float _max;

        // コンストラクタ
        public Timer(float max = 1)
        {
            _time = 0;
            _max = max;
        }
        public float Time
        {
            get { return _time; }
        }
        // カウント
        public void Counter(float time)
        {
            _time = Mathf.Min(_time + time, _max);
        }
        // クリア
        public void Clear()
        {
            _time = 0;
        }
    }
}
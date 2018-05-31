using System.Collections;
using UnityEngine;
using Dimension.Player;
using Dimension.Camera2D3D.Effect;

namespace Dimension.Camera2D3D
{
    public class CameraController : MonoBehaviour
    {
        public TestPlayer player;
        CameraWork cameraWork;
        PostEffect postEffect;

        //=====================================================
        void Start()
        {
            if (cameraWork == null) ChangeWork<CameraWork3D>();
            postEffect = GetComponent<PostEffect>();
        }
        void FixedUpdate()
        {
            if (CheckWork()) cameraWork.Move();
        }
        //-----------------------------------------------------
        //  カメラワークを変更
        //-----------------------------------------------------
        public void ChangeWork<CW>() where CW : CameraWork
        {
            Destroy(cameraWork);
            cameraWork = gameObject.AddComponent<CW>();
            cameraWork.CameraTarget = player.transform;
            cameraWork.Initialize();
        }
        //-----------------------------------------------------
        //  Cameraが動作可能か
        //-----------------------------------------------------
        bool CheckWork()
        {
            return cameraWork != null && cameraWork.enabled;
        }
        //-----------------------------------------------------
        //  フェードアウト
        //-----------------------------------------------------
        public IEnumerator FadeOutCoroutine()
        {
            yield return StartCoroutine(postEffect.FadeCoroutine(Fade.OUT));
        }
        //-----------------------------------------------------
        //  フェードイン
        //-----------------------------------------------------
        public IEnumerator FadeInCoroutine()
        {
            yield return StartCoroutine(postEffect.FadeCoroutine(Fade.IN));
        }
        //-----------------------------------------------------
        // ポストエフェクト
        //-----------------------------------------------------
        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (postEffect == null) return;
            Graphics.Blit(src, dest, postEffect.EffectMaterial);
        }
    }
}
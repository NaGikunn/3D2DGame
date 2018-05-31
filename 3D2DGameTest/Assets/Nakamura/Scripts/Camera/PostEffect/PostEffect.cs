using System.Collections;
using UnityEngine;

namespace Dimension.Camera2D3D.Effect
{
    public enum Fade
    {
        IN = 1,
        OUT = -1
    }

    public class PostEffect : MonoBehaviour
    {
        //アスペクト比
        const float ASPECT_WIDTH = 1.6f;
        const float ASPECT_HEIGHT = 0.9f;

        const float RADIUS_MIN = 0;
        const float RADIUS_MAX = 1.5f;

        [SerializeField] Material _effectMaterial;
        [SerializeField] Transform target;

        float _radius;

        void Awake()
        {
            Radius = RADIUS_MAX;
            _effectMaterial.SetVector("_Aspect", new Vector2(ASPECT_WIDTH, ASPECT_HEIGHT));
            _effectMaterial.SetVector("_WipePos", WipeCenter());
        }
        //-----------------------------------------------------
        //  プロパティ
        //-----------------------------------------------------
        public Material EffectMaterial
        {
            get { return _effectMaterial; }
        }
        public float Radius
        {
            get { return _radius; }
            private set
            {
                _radius = Mathf.Clamp(value, RADIUS_MIN, RADIUS_MAX);
                _effectMaterial.SetFloat("_Radius", _radius);
            }
        }
        //-----------------------------------------------------
        //  フェード
        //-----------------------------------------------------
        public IEnumerator FadeCoroutine(Fade dir)
        {
            _effectMaterial.SetVector("_WipePos", WipeCenter());

            float max = (dir == Fade.OUT) ? RADIUS_MIN : RADIUS_MAX;
            float sa = RADIUS_MIN - RADIUS_MAX;
            while (Radius != max)
            {
                Radius += (int)dir * 0.03f;
                yield return new WaitForSeconds(sa / Time.deltaTime);
            }
        }
        //-----------------------------------------------------
        //  ワイプの中心座標
        //-----------------------------------------------------
        Vector2 WipeCenter()
        {
            const float TWEAK_NUM = 0.18f;  // 調整用の値

            Vector2 wipePos;
            if (target == null) wipePos = new Vector2(0.5f, 0.5f);
            else wipePos = Camera.main.WorldToViewportPoint(target.position);

            wipePos.x *= ASPECT_WIDTH + TWEAK_NUM;
            wipePos.y *= ASPECT_HEIGHT + TWEAK_NUM;
            return wipePos;
        }
    }
}
Shader "Custom/WipeCircle" {
	Properties{
		_Radius("Radius", Range(0,1.5)) = 1.5
		_MainTex("MainTex", 2D) = ""{}
	}
	SubShader{
		Pass{
			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert_img
			#pragma fragment frag

			float _Radius;
			sampler2D _MainTex;
			fixed2 _Aspect;
			fixed2 _WipePos;

			fixed4 frag(v2f_img i) : COLOR{
				fixed4 c = tex2D(_MainTex, i.uv);
				i.uv -= fixed2(0.0, 0.0);
				i.uv.x *= _Aspect.x / _Aspect.y;
				if (distance(i.uv, _WipePos) > _Radius) {
					c = fixed4(0.0, 0.0, 0.0, 1.0);
				}
				return c;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}

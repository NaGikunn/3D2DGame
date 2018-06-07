Shader "Custom/Grass" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_Alpha("Alpha", Range(0,1)) = 1
	}
	SubShader{
		Tags {"Queue" = "Transparent"}
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard alpha:fade
		#pragma target 3.0

		fixed4 _Color;
		fixed _Alpha;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			o.Albedo = _Color;
			o.Alpha = _Alpha;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

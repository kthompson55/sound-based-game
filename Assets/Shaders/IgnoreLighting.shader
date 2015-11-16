Shader "Custom/IgnoreLighting" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf NoLighting

		sampler2D _MainTex;

		// Custom light model that ignores actual lighting. 
		half4 LightingNoLighting (SurfaceOutput s, half3 lightDir, half atten) {
			half4 c;
			c.rgb = s.Albedo;
			c.a = s.Alpha;
			return c;
		}

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = _Color;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}

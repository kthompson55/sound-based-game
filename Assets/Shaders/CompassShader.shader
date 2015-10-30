Shader "Custom/Echo/Compass" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_MainColor ("Main Color",Color) = (1.0,1.0,1.0,1.0)	
		_MaxFade("Max Fade",float) = 0.0  
		_DistanceFade("Distance Fade",float) = 1.0	
		
		_Position0("Position0", Vector) = (0.0,0.0,0.0)
		_Radius0("Radius0",float) = 0.0
		_MaxRadius0("Radius0", float) = 0.0
		_Fade0("Echo Fade0",float) = 0.0

		_Position1("Position1",Vector) = (0.0,0.0,0.0)
		_Radius1("Radius1",float) = 0.0
		_MaxRadius1("Max Radius1",float) = 0.0
		_Fade1("Echo Fade1", float) = 0.0

	} 
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf NoLighting
		#include "UnityCG.cginc"
		
		struct Input {
			float2 uv_MainTex;
			float3 worldPos;	
		};
		
		sampler2D _MainTex;
		sampler2D _EchoTex;
		
		float4 _MainColor;
		float  _MaxFade;
		float _DistanceFade;
		

		float3 _Position0;
		float  _Radius0;
		float  _MaxRadius0;
		float  _Fade0;

		float3 _Position1;
		float  _Radius1;
		float  _MaxRadius1;
		float  _Fade1;

		// Custom light model that ignores actual lighting. 
		half4 LightingNoLighting (SurfaceOutput s, half3 lightDir, half atten) {
			half4 c;
			c.rgb = s.Albedo;
			c.a = s.Alpha;
			return c;
		}
		
		float ApplyFade(Input IN, float3 position, float radius, float fade, float maxRadius){
			float dist = distance(IN.worldPos, position);	// Distance from current pixel (from its world coord) to center of echo sphere
			
			if(radius >= 3*maxRadius || dist >= radius){
				return 0.0;
			} else {

				float c1 = (_DistanceFade>=1.0)?dist/radius:1.0;
				
				c1 *= (fade<=_MaxFade)?1.0-fade/_MaxFade:0.0;	
				
				c1 = (fade<=0)?1.0:c1;				
				
				return  c1;
			} 
		}
		
		// Custom surfacer that mimics an echo effect
		void surf (Input IN, inout SurfaceOutput o) {
			float c1 = 0.0;
			
				c1 +=ApplyFade(IN, _Position0,_Radius0,_Fade0,_MaxRadius0);
				c1 +=ApplyFade(IN, _Position1,_Radius1,_Fade1,_MaxRadius1);
							
			
			//makes all echoes lighter
			//c1 /= 9.0;			

			float c2 = 1.0 - c1;

			o.Albedo = _MainColor * c2 + tex2D(_MainTex, IN.uv_MainTex).rgb*c1;
		}
		ENDCG
		
	} 
	FallBack "Diffuse"
}
Shader "Custom/Echo/Multi" {
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

		_Position2("Position2",Vector) = (0.0,0.0,0.0)
		_Radius2("Radius2",float) = 0.0
		_MaxRadius2("Max Radius2",float) = 0.0
		_Fade2("Echo Fade2",float) = 0.0

		
		_Position3("Position3",Vector) = (0.0,0.0,0.0)
		_Radius3("Radius3",float) = 0.0
		_MaxRadius3("Max Radius3",float) = 0.0
		_Fade3("Echo Fade3",float) = 0.0
		
		_Position4("Position4",Vector) = (0.0,0.0,0.0)
		_Radius4("Radius4",float) = 0.0
		_MaxRadius4("Max Radius4",float) = 0.0
		_Fade4("Echo Fade4",float) = 0.0
		
		_Position5("Position5",Vector) = (0.0,0.0,0.0)
		_Radius5("Radius5",float) = 0.0
		_MaxRadius5("Max Radius5",float) = 0.0
		_Fade5("Echo Fade5",float) = 0.0
		
		_Position6("Position6",Vector) = (0.0,0.0,0.0)
		_Radius6("Radius6",float) = 0.0
		_MaxRadius6("Max Radius6",float) = 0.0
		_Fade6("Echo Fade6",float) = 0.0
		
		_Position7("Position7",Vector) = (0.0,0.0,0.0)
		_Radius7("Radius7",float) = 0.0
		_MaxRadius7("Max Radius7",float) = 0.0
		_Fade7("Echo Fade7",float) = 0.0
		
		_Position8("Position8",Vector) = (0.0,0.0,0.0)
		_Radius8("Radius8",float) = 0.0
		_MaxRadius8("Max Radius8",float) = 0.0
		_Fade8("Echo Fade8",float) = 0.0
		
		_Position9("Position9",Vector) = (0.0,0.0,0.0)
		_Radius9("Radius9",float) = 0.0
		_MaxRadius9("Max Radius9",float) = 0.0
		_Fade9("Echo Fade9",float) = 0.0

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

		float3 _Position2;
		float  _Radius2;
		float  _MaxRadius2;
		float  _Fade2;

		float3 _Position3;
		float  _Radius3;
		float  _MaxRadius3;
		float  _Fade3;

		float3 _Position4;
		float  _Radius4;
		float  _MaxRadius4;
		float  _Fade4;

		float3 _Position5;
		float  _Radius5;
		float  _MaxRadius5;
		float  _Fade5;

		float3 _Position6;
		float  _Radius6;
		float  _MaxRadius6;
		float  _Fade6;

		float3 _Position7;
		float  _Radius7;
		float  _MaxRadius7;
		float  _Fade7;

		float3 _Position8;
		float  _Radius8;
		float  _MaxRadius8;
		float  _Fade8;

		float3 _Position9;
		float  _Radius9;
		float  _MaxRadius9;
		float  _Fade9;
		
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
				c1 +=ApplyFade(IN, _Position2,_Radius2,_Fade2,_MaxRadius2);
				c1 +=ApplyFade(IN, _Position3,_Radius3,_Fade3,_MaxRadius3);
				c1 +=ApplyFade(IN, _Position4,_Radius4,_Fade4,_MaxRadius4);
				c1 +=ApplyFade(IN, _Position5,_Radius5,_Fade5,_MaxRadius5);
				c1 +=ApplyFade(IN, _Position6,_Radius6,_Fade6,_MaxRadius6);
				c1 +=ApplyFade(IN, _Position7,_Radius7,_Fade7,_MaxRadius7);
				c1 +=ApplyFade(IN, _Position8,_Radius8,_Fade8,_MaxRadius8);
				c1 +=ApplyFade(IN, _Position9,_Radius9,_Fade9,_MaxRadius9);
			
			
			//makes all echoes lighter
			//c1 /= 9.0;			

			float c2 = 1.0 - c1;

			o.Albedo = _MainColor * c2 + tex2D(_MainTex, IN.uv_MainTex).rgb*c1;
		}
		ENDCG
		
	} 
	FallBack "Diffuse"
}

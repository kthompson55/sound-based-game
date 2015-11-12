Shader "Custom/Echo/Multi" {
	Properties {
		_MainColor ("Main Color",Color) = (1.0,1.0,1.0,1.0)	
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_MaxFade("Max Fade",float) = 0.0  
		_DistanceFade("Distance Fade",float) = 1.0	
		
		_Position0("Position0", Vector) = (0.0,0.0,0.0)
		_Radius0("Radius0",float) = 0.0
		_MaxRadius0("Radius0", float) = 0.0
		_Fade0("Echo Fade0",float) = 0.0
		_MainColor0 ("Main Color0",Color) = (0.0,0.0,0.0,1.0)	


		_Position1("Position1",Vector) = (0.0,0.0,0.0)
		_Radius1("Radius1",float) = 0.0
		_MaxRadius1("Max Radius1",float) = 0.0
		_Fade1("Echo Fade1", float) = 0.0
		_MainColor1 ("Main Color1",Color) = (0.0,0.0,0.0,1.0)	


		_Position2("Position2",Vector) = (0.0,0.0,0.0)
		_Radius2("Radius2",float) = 0.0
		_MaxRadius2("Max Radius2",float) = 0.0
		_Fade2("Echo Fade2",float) = 0.0
		_MainColor2 ("Main Color2",Color) = (0.0,0.0,0.0,1.0)	


		
		_Position3("Position3",Vector) = (0.0,0.0,0.0)
		_Radius3("Radius3",float) = 0.0
		_MaxRadius3("Max Radius3",float) = 0.0
		_Fade3("Echo Fade3",float) = 0.0
		_MainColor3 ("Main Color3",Color) = (0.0,0.0,0.0,1.0)	

		
		_Position4("Position4",Vector) = (0.0,0.0,0.0)
		_Radius4("Radius4",float) = 0.0
		_MaxRadius4("Max Radius4",float) = 0.0
		_Fade4("Echo Fade4",float) = 0.0
		_MainColor4 ("Main Color4",Color) = (0.0,0.0,0.0,1.0)	

		
		_Position5("Position5",Vector) = (0.0,0.0,0.0)
		_Radius5("Radius5",float) = 0.0
		_MaxRadius5("Max Radius5",float) = 0.0
		_Fade5("Echo Fade5",float) = 0.0
		_MainColor5 ("Main Color5",Color) = (0.0,0.0,0.0,1.0)	

		
		_Position6("Position6",Vector) = (0.0,0.0,0.0)
		_Radius6("Radius6",float) = 0.0
		_MaxRadius6("Max Radius6",float) = 0.0
		_Fade6("Echo Fade6",float) = 0.0
		_MainColor6 ("Main Color6",Color) = (0.0,0.0,0.0,1.0)	

		
		_Position7("Position7",Vector) = (0.0,0.0,0.0)
		_Radius7("Radius7",float) = 0.0
		_MaxRadius7("Max Radius7",float) = 0.0
		_Fade7("Echo Fade7",float) = 0.0
		_MainColor7 ("Main Color7",Color) = (0.0,0.0,0.0,1.0)	

		
		_Position8("Position8",Vector) = (0.0,0.0,0.0)
		_Radius8("Radius8",float) = 0.0
		_MaxRadius8("Max Radius8",float) = 0.0
		_Fade8("Echo Fade8",float) = 0.0
		_MainColor8 ("Main Color8",Color) = (0.0,0.0,0.0,1.0)	

		
		_Position9("Position9",Vector) = (0.0,0.0,0.0)
		_Radius9("Radius9",float) = 0.0
		_MaxRadius9("Max Radius9",float) = 0.0
		_Fade9("Echo Fade9",float) = 0.0
		_MainColor9 ("Main Color9",Color) = (0.0,0.0,0.0,1.0)	


		_Position10("Position10",Vector) = (0.0,0.0,0.0)
		_Radius10("Radius10",float) = 0.0
		_MaxRadius10("Max Radius10",float) = 0.0
		_Fade10("Echo Fade10",float) = 0.0
		_MainColor10 ("Main Color10",Color) = (0.0,0.0,0.0,1.0)	


		_Position11("Position11",Vector) = (0.0,0.0,0.0)
		_Radius11("Radius11",float) = 0.0
		_MaxRadius11("Max Radius11",float) = 0.0
		_Fade11("Echo Fade11",float) = 0.0
		_MainColor11 ("Main Color11",Color) = (0.0,0.0,0.0,1.0)	


		_Position12("Position12",Vector) = (0.0,0.0,0.0)
		_Radius12("Radius12",float) = 0.0
		_MaxRadius12("Max Radius12",float) = 0.0
		_Fade12("Echo Fade12",float) = 0.0
		_MainColor12 ("Main Color12",Color) = (0.0,0.0,0.0,1.0)	


		_Position13("Position13",Vector) = (0.0,0.0,0.0)
		_Radius13("Radius13",float) = 0.0
		_MaxRadius13("Max Radius13",float) = 0.0
		_Fade13("Echo Fade13",float) = 0.0
		_MainColor13 ("Main Color13",Color) = (0.0,0.0,0.0,1.0)	


		_Position14("Position14",Vector) = (0.0,0.0,0.0)
		_Radius14("Radius14",float) = 0.0
		_MaxRadius14("Max Radius14",float) = 0.0
		_Fade14("Echo Fade14",float) = 0.0
		_MainColor14 ("Main Color14",Color) = (0.0,0.0,0.0,1.0)	


		_Position15("Position15",Vector) = (0.0,0.0,0.0)
		_Radius15("Radius15",float) = 0.0
		_MaxRadius15("Max Radius15",float) = 0.0
		_Fade15("Echo Fade15",float) = 0.0
		_MainColor15 ("Main Color15",Color) = (0.0,0.0,0.0,1.0)	


		_Position16("Position16",Vector) = (0.0,0.0,0.0)
		_Radius16("Radius16",float) = 0.0
		_MaxRadius16("Max Radius16",float) = 0.0
		_Fade16("Echo Fade16",float) = 0.0
		_MainColor16 ("Main Color16",Color) = (0.0,0.0,0.0,1.0)	


		_Position17("Position17",Vector) = (0.0,0.0,0.0)
		_Radius17("Radius17",float) = 0.0
		_MaxRadius17("Max Radius17",float) = 0.0
		_Fade17("Echo Fade17",float) = 0.0
		_MainColor17 ("Main Color17",Color) = (0.0,0.0,0.0,1.0)	


		_Position18("Position18",Vector) = (0.0,0.0,0.0)
		_Radius18("Radius18",float) = 0.0
		_MaxRadius18("Max Radius18",float) = 0.0
		_Fade18("Echo Fade18",float) = 0.0
		_MainColor18 ("Main Color18",Color) = (0.0,0.0,0.0,1.0)	


		_Position19("Position19",Vector) = (0.0,0.0,0.0)
		_Radius19("Radius19",float) = 0.0
		_MaxRadius19("Max Radius19",float) = 0.0
		_Fade19("Echo Fade19",float) = 0.0
		_MainColor19 ("Main Color19",Color) = (0.0,0.0,0.0,1.0)	


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
		float4 _MainColor0;


		float3 _Position1;
		float  _Radius1;
		float  _MaxRadius1;
		float  _Fade1;
		float4 _MainColor1;


		float3 _Position2;
		float  _Radius2;
		float  _MaxRadius2;
		float  _Fade2;
		float4 _MainColor2;


		float3 _Position3;
		float  _Radius3;
		float  _MaxRadius3;
		float  _Fade3;
		float4 _MainColor3;


		float3 _Position4;
		float  _Radius4;
		float  _MaxRadius4;
		float  _Fade4;
		float4 _MainColor4;


		float3 _Position5;
		float  _Radius5;
		float  _MaxRadius5;
		float  _Fade5;
		float4 _MainColor5;


		float3 _Position6;
		float  _Radius6;
		float  _MaxRadius6;
		float  _Fade6;
		float4 _MainColor6;

		float3 _Position7;
		float  _Radius7;
		float  _MaxRadius7;
		float  _Fade7;
		float4 _MainColor7;

		float3 _Position8;
		float  _Radius8;
		float  _MaxRadius8;
		float  _Fade8;
		float4 _MainColor8;

		float3 _Position9;
		float  _Radius9;
		float  _MaxRadius9;
		float  _Fade9;
		float4 _MainColor9;

		float3 _Position10;
		float  _Radius10;
		float  _MaxRadius10;
		float  _Fade10;
		float4 _MainColor10;

		float3 _Position11;
		float  _Radius11;
		float  _MaxRadius11;
		float  _Fade11;
		float4 _MainColor11;

		float3 _Position12;
		float  _Radius12;
		float  _MaxRadius12;
		float  _Fade12;
		float4 _MainColor12;

		float3 _Position13;
		float  _Radius13;
		float  _MaxRadius13;
		float  _Fade13;
		float4 _MainColor13;

		float3 _Position14;
		float  _Radius14;
		float  _MaxRadius14;
		float  _Fade14;
		float4 _MainColor14;

		float3 _Position15;
		float  _Radius15;
		float  _MaxRadius15;
		float  _Fade15;
		float4 _MainColor15;

		float3 _Position16;
		float  _Radius16;
		float  _MaxRadius16;
		float  _Fade16;
		float4 _MainColor16;

		float3 _Position17;
		float  _Radius17;
		float  _MaxRadius17;
		float  _Fade17;
		float4 _MainColor17;

		float3 _Position18;
		float  _Radius18;
		float  _MaxRadius18;
		float  _Fade18;
		float4 _MainColor18;

		float3 _Position19;
		float  _Radius19;
		float  _MaxRadius19;
		float  _Fade19;
		float4 _MainColor19;
		
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
			float c2 = 0.0;
			float c3 = 0.0;
			float c4 = 0.0;
			float c5 = 0.0;
			float c6 = 0.0;
			float c7 = 0.0;
			float c8 = 0.0;
			float c9 = 0.0;
			float c10 = 0.0;
			float c11 = 0.0;
			float c12 = 0.0;
			float c13 = 0.0;
			float c14 = 0.0;
			float c15 = 0.0;
			float c16 = 0.0;
			float c17 = 0.0;
			float c18 = 0.0;
			float c19 = 0.0;
			float c20 = 0.0;
			


			c1 =ApplyFade(IN, _Position0,_Radius0,_Fade0,_MaxRadius0);
			c2 =ApplyFade(IN, _Position1,_Radius1,_Fade1,_MaxRadius1);
			c3 =ApplyFade(IN, _Position2,_Radius2,_Fade2,_MaxRadius2);
			c4 =ApplyFade(IN, _Position3,_Radius3,_Fade3,_MaxRadius3);
			c5 =ApplyFade(IN, _Position4,_Radius4,_Fade4,_MaxRadius4);
			c6 =ApplyFade(IN, _Position5,_Radius5,_Fade5,_MaxRadius5);
			c7 =ApplyFade(IN, _Position6,_Radius6,_Fade6,_MaxRadius6);
			c8 =ApplyFade(IN, _Position7,_Radius7,_Fade7,_MaxRadius7);
			c9 =ApplyFade(IN, _Position8,_Radius8,_Fade8,_MaxRadius8);
			c10 =ApplyFade(IN, _Position9,_Radius9,_Fade9,_MaxRadius9);
			c11 =ApplyFade(IN, _Position10,_Radius10,_Fade10,_MaxRadius10);
			c12 =ApplyFade(IN, _Position11,_Radius11,_Fade11,_MaxRadius11);
			c13 =ApplyFade(IN, _Position12,_Radius12,_Fade12,_MaxRadius12);
			c14 =ApplyFade(IN, _Position13,_Radius13,_Fade13,_MaxRadius13);
			c15 =ApplyFade(IN, _Position14,_Radius14,_Fade14,_MaxRadius14);
			c16 =ApplyFade(IN, _Position15,_Radius15,_Fade15,_MaxRadius15);
			c17 =ApplyFade(IN, _Position16,_Radius16,_Fade16,_MaxRadius16);
			c18 =ApplyFade(IN, _Position17,_Radius17,_Fade17,_MaxRadius17);
			c19 =ApplyFade(IN, _Position18,_Radius18,_Fade18,_MaxRadius18);
			c20 =ApplyFade(IN, _Position19,_Radius19,_Fade19,_MaxRadius19);
			
			
			//makes all echoes lighter
			//c1 /= 9.0;			

			//float c2 = 1.0 - c1;

			float t = 0.0;
			t= c1 +c2 +c3 +c4 +c5 +c6 +c7 +c8 +c9 +c10 +c11 +c12 +c13 +c14 +c15 +c16 +c17 +c18 +c19 +c20; 
			float t1 = 1.0 - t;

			float f1 = 1.0 - c1;
			float f2 = 1.0 - c2;
			float f3 = 1.0 - c3;
			float f4 = 1.0 - c4;
			float f5 = 1.0 - c5;
			float f6 = 1.0 - c6;
			float f7 = 1.0 - c7;
			float f8 = 1.0 - c8;
			float f9 = 1.0 - c9;
			float f10 = 1.0 - c10;
			float f11 = 1.0 - c11;
			float f12 = 1.0 - c12;
			float f13 = 1.0 - c13;
			float f14 = 1.0 - c14;
			float f15 = 1.0 - c15;
			float f16 = 1.0 - c16;
			float f17 = 1.0 - c17;
			float f18 = 1.0 - c18;
			float f19 = 1.0 - c19;
			float f20 = 1.0 - c20;
			
			float3 temp = _MainColor0 * f1 +tex2D(_MainTex, IN.uv_MainTex).rgb * (c1);
			o.Albedo = (_MainColor * t1) + tex2D(_MainTex, IN.uv_MainTex).rgb *t;
			o.Albedo += _MainColor0 * c1;
			o.Albedo += _MainColor1 * (c2);
			o.Albedo += _MainColor2 * c3;
			o.Albedo += _MainColor3 * c4;
			o.Albedo += _MainColor4 * c5;
			o.Albedo += _MainColor5 * c6;
			o.Albedo += _MainColor6 * c7;
			o.Albedo += _MainColor7 * c8;
			o.Albedo += _MainColor8 * c9;
			o.Albedo += _MainColor9 *  c10;
			o.Albedo += _MainColor10 * c11;
			o.Albedo += _MainColor11 * c12;
			o.Albedo += _MainColor12 * c13;
			o.Albedo += _MainColor13 * c14;
			o.Albedo += _MainColor14 * c15;
			o.Albedo += _MainColor15 * c16;
			o.Albedo += _MainColor16 * c17;
			o.Albedo += _MainColor17 * c18;
			o.Albedo += _MainColor18 * c19;
			o.Albedo += _MainColor19 * c20;

			
		}
		ENDCG
		
	} 
	FallBack "Diffuse"
}

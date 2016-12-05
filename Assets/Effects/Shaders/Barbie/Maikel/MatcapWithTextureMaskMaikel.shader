Shader "Maikel/MatCap/MatCap Texture Mask" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "black" {}	
		_MatCap ("MatCap (RGB)", 2D) = "black" {}
		_Intensity(" Intensity", FLOAT) = 2.0
		_Mask ("MatCap Intensity Mask (GRAYSCALE)",2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
	}
	
	Subshader {
		Tags { "RenderType"="Opaque" }
		Fog { Color [_AddFog] }
		
		Pass {
			Name "BASE"
			Tags { "LightMode" = "ForwardBase" }
			
			CGPROGRAM
				#pragma exclude_renderers xbox360
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_fog_exp2
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"
				
				struct v2f { 
					half4 	pos : SV_POSITION;
					half2	uv : TEXCOORD0;
					half3	TtoV0 : TEXCOORD1;
					half3	TtoV1 : TEXCOORD2;
					half3 	n : TEXCOORD3;
				};
				
				v2f vert (appdata_tan v)
				{
					v2f o;
					o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
					o.uv = v.texcoord;
					
					TANGENT_SPACE_ROTATION;
					o.n = mul(rotation, v.normal);
					o.TtoV0 = mul(rotation, UNITY_MATRIX_IT_MV[0].xyz);
					o.TtoV1 = mul(rotation, UNITY_MATRIX_IT_MV[1].xyz);
					
					return o;
				}
				
				uniform sampler2D _MainTex;
				uniform sampler2D _MatCap;
				uniform sampler2D _Mask;
				uniform float _Intensity;
				float4 _Color;				
				
				float4 frag (v2f i) : COLOR
				{
					half2 vn;
					vn.x = dot(i.TtoV0, i.n);
					vn.y = dot(i.TtoV1, i.n);

					fixed4 matcapLookup  = tex2D(_MatCap, vn*0.5 + 0.5);
					fixed4 mainTextColor = tex2D(_MainTex,i.uv);
					
					fixed4 maskValue = tex2D(_Mask, i.uv);
					matcapLookup = (matcapLookup*(maskValue.g)) *_Intensity;
					
					
					return (mainTextColor + (matcapLookup) * mainTextColor.a) * _Color;
				}
			ENDCG
		}
	}
}
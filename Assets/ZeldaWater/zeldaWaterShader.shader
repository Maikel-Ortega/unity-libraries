Shader "Maikel/Unlit/ZeldaWaterShader"
{
	Properties
	{
		_TileableMask ("WaveTileMask", 2D) = "white" {}
		_WaterColor ("Water Color", Color) = (0,0,0,1)
		_FoamColor ("Foam Color", Color) = (0,0,0,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _TileableMask;
			float4 _TileableMask_ST;
			float4 _WaterColor;
			float4 _FoamColor;


			v2f vert (appdata v)
			{
				v2f o;
				//v.vertex.y += 0.2f* sin(5*v.vertex.x*_Time[0])+ 0.2f* sin(2*v.vertex.z*_Time[0]);
				o.vertex =  mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _TileableMask);
				//o.uv.x += 0.1* sin(2*v.vertex.x*_Time[0]);
				//o.uv.y += 0.05* sin(0.5*v.vertex.z*_Time[0]);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				i.uv.y += sin(i.uv.x*0.1*_Time[0]);
				fixed4 col = tex2D(_TileableMask, i.uv);
				col = 1-col;
				col *= _WaterColor;
				if(col.b < _WaterColor.b)
				{
					col = _FoamColor;
				}
				return col;
			}
			ENDCG
		}
	}
}

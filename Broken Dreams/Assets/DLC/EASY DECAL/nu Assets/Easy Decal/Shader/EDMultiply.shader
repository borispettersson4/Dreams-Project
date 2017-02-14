﻿// Easy Decal unlit multiply shader. version 1.0
Shader "Easy Decal/Unlit/ED Multiply"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
		LOD 100
		Blend DstColor Zero
		Offset -1,-1
		Lighting Off


		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.color = v.color;

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 color = tex2D(_MainTex, i.uv) * _Color * i.color;		
						
				fixed a = color.a;
				fixed ainv = 1.0 - color.a;

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, color);

				color =  (color * a) + (fixed4(1, 1, 1, 1) * ainv);

				return color;
			}
			ENDCG
		}
	}
}

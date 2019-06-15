
Shader "SpriteShader/SpriteS"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_PlayerPos("PlayerPos", Vector) = (0,0,0,0)
		_MaxDistance("maxDistance", Float) = 0.0
		_CutOff("cutt off", Range(0.165, 1.55)) = 0.0
		_ColorCut("CutOffColor", Color) = (0,0,0,0)
		_Color("uColor", Color) = (0,0,0,0)
	}

	SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
			"Queue" = "Transparent+1"
		}

		Pass
		{
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON

			sampler2D _MainTex;
			//float4 _MainTex_ST;
			float4 _PlayerPos;
			float4 _Color;
			float4 _ColorCut;
			float _CutOff;
			float _MaxDistance;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv_MainTex : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
			};

			struct v2f
			{
				float4 vertex : POSITION;
				float2 uv_MainTex : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				float4 color : COLOR;
			};

			v2f vert(appdata v)
			{
				v2f o;

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv_MainTex = v.uv_MainTex;
				o.uv2 = v.uv2;

				float dist = distance(_PlayerPos, v.vertex);
				float safety = step(0.001, _MaxDistance);

				o.color = _Color;

				float val = (1 - clamp(pow(dist, _CutOff), 0, _MaxDistance) / _MaxDistance);
				o.color = lerp(o.color, _ColorCut, 1 - val) * val;

				return o;
			}

			float4 frag(v2f i) : COLOR
			{
				float4 o = float4(1, 1, 1, 1);

				half4 c = tex2D(_MainTex, i.uv_MainTex);
				o.rgb = c.rgb * i.color;
				o.a = c.a;
			
				return o;
			}

			ENDCG
		}
	}
}
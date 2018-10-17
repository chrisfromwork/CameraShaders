Shader "Voronoi"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Alpha ("Alpha", Float) = 1.0
	}
	SubShader
	{
		Cull Off ZWrite Off ZTest Always

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
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Alpha;
			int _PointsCount;
			float _Points[4056];
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float4 color = tex2D(_MainTex, i.uv);

				if (_PointsCount == 0)
					return color;

				// Find the closest point to the voronoi region point.
				float minDist = distance(i.uv, _Points[0]);
				int minIndex = 0;

				for (int n = 1; n < _PointsCount; n+=2)
				{
					float currDist = distance(i.uv, float2(_Points[n], _Points[n + 1]));
					if (currDist < minDist)
					{
						minIndex = n;
						minDist = currDist;
					}
				}

				// Return a blended color based on the voronoi region point and the actual color.
				return (_Alpha * tex2D(_MainTex, float2(_Points[minIndex], _Points[minIndex + 1]))) + ((1.0f - _Alpha) * color);
			}
			ENDCG
		}
	}
}

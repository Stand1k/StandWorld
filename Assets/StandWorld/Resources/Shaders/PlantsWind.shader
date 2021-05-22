Shader "StandWorld/Plants Wind"
 {
	Properties 
	{
		_MainTex ("Main texture", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
		_MinStrength ("_MinStrength", Range(0,1)) = 0
		_MaxStrength ("_MaxStrength", Range(0,1)) = 0
		_StrengthScale ("StrengthScale", float) = 1
		_Interval ("Interval", float) = 3.5
		_Detail ("Detail", float) = 1
		_Distortion ("Distortion", Range(0,1)) = 0
		_HeightOffset ("HeightOffset", float) = 0
		_Offset ("Offset", float) = 0
		
	}

	SubShader 
	{
		Cull Back
		Lighting Off
		ZWrite Off
		Tags { "Queue"="Transparent"  }
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 200

		Pass 
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			float _Speed = 1;
			float _MinStrength;
			float _MaxStrength;
			float _StrengthScale;
			float _Interval;
			float _Detail;
			float _Distortion;
			float _HeightOffset;

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			float4 getWind(float2 vertex, float2 uv, float timer)
			{
				float2 pos = mul(vertex, _Distortion / 100);
				float time = timer * _Speed + pos.x * pos.y * 100;
				float4 diff = pow(_MaxStrength + 0.2 , 2.0);
				float4 strength = clamp(_MinStrength + diff + sin(time / _Interval) * diff, _MinStrength, _MaxStrength) * _StrengthScale;
				float4 wind = (sin(time) + cos(time * _Detail)) * strength  * max(0.0, (1.0-uv.y) - _HeightOffset) * 5;
			
				return wind;
			}


			v2f vert(appdata_full v)
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				v.vertex.x += getWind(v.vertex, -v.texcoord, _Time.z);
				v.vertex.y += getWind(v.vertex, -v.texcoord, _Time.z) / 3;

				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 outDiffuse = tex2D(_MainTex,  i.uv) * _Color;
				return outDiffuse;
			}

			ENDCG
		} 
	}
}
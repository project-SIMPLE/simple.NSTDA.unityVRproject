Shader "ToonLight" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Ramp("Toon Ramp (RGB)", 2D) = "gray" {}
	}
		SubShader{
		Cull Off
			Tags { "RenderType" = "Opaque" }
			LOD 200
			CGPROGRAM
			#pragma surface surf ToonRamp

			sampler2D _Ramp;

	// custom lighting function that uses a texture ramp based
	// on angle between light direction and normal
	#pragma lighting ToonRamp exclude_path:prepass
	inline half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half atten)
	{
		#ifndef USING_DIRECTIONAL_LIGHT
		lightDir = normalize(lightDir);
		#endif

		half d = (dot(s.Normal, lightDir)* 0.5 + 0.5) * (atten);
		half3 ramp = tex2D(_Ramp, float2(d,d)).rgb;

		half4 c;
		c.rgb = (s.Albedo * 2) * _LightColor0.rgb * ramp; // * (atten * 2);
		c.a = 0;
		return c;
	}

	sampler2D _MainTex;

	//float4 _Color;

	struct Input {
		float2 uv_MainTex : TEXCOORD0;
	};

	UNITY_INSTANCING_BUFFER_START(Props)
	   UNITY_DEFINE_INSTANCED_PROP(half4, _Color)
	UNITY_INSTANCING_BUFFER_END(Props)

	void surf(Input IN, inout SurfaceOutput o) {
		half4 c = tex2D(_MainTex, IN.uv_MainTex) * UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG
	}

		Fallback "Diffuse"
}
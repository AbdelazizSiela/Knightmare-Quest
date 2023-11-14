Shader "Custom/Wobbling"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _WobbleStrength ("Wobble Strength", Range(0, 0.1)) = 0.02
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
        };

        float _WobbleStrength;

        void surf(Input IN, inout SurfaceOutput o)
        {
            // Wobble effect
            float2 uv = IN.uv_MainTex;
            uv += _WobbleStrength * sin(uv.y * 10) * sin(uv.x * 10);

            // Sample the texture
            fixed4 c = tex2D(_MainTex, uv);

            o.Albedo = c.rgb;
            o.Alpha = c.a * _Color.a;
        }
        ENDCG
    }
}

Shader "Unlit/Final"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            float sv(float2 uv) { return length(tex2D(_MainTex, uv).xyz); }
            float2 g(float2 uv, float e) {
                return float2(sv(uv + float2(e, 0.)) - sv(uv - float2(e, 0.)), sv(uv + float2(0., e)) - sv(uv - float2(0., e))) / e;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
            float3 n = float3(g(i.uv, 0.001), 200.);
            n = normalize(n);
            float3 li = float3(0.5, 0.5, 1.);
            float sha = clamp(dot(n, li), 0., 1.0);
                return col*sha;
            }
            ENDCG
        }
    }
}

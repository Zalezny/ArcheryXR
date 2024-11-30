Shader "Unlit/BlackHoleShader"
{
    Properties
    {
        _Color ("Circle Color", Color) = (0, 0, 0, 1)
        _Radius ("Radius", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }
        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
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
            float4 _Color;
            float _Radius;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 center = float2(0.5, 0.5); // Center of the circle
                float dist = distance(i.uv, center);
                float alpha = step(_Radius, dist); // Create radial effect
                return float4(_Color.rgb, 1 - alpha);
            }
            ENDCG
        }
    }
}

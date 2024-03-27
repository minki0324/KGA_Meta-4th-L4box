Shader "Unlit/testShader_2"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _PlaneColor("Plane Color", Color) = (1,1,1,1)
        _Visibility("Visibility", Range(0.001, 10)) = 1
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Overlay" "PreviewType" = "Plane" }
            LOD 100

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 2.0

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct v2f
                {
                    float4 vertex : SV_POSITION;
                    float2 uv : TEXCOORD0;
                };

                sampler2D _MainTex;
                float4 _PlaneColor;
                float _Visibility;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    float alpha = 1 - smoothstep(_Visibility, _Visibility * 0.01, i.uv.y);
                    col.rgb = lerp(_PlaneColor.rgb, col.rgb, col.a);
                    col.a *= alpha;
                    return col;
                }
                ENDCG
            }
        }
}
Shader "Unlit/testShader_1"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _PlaneColor("Plane Color", Color) = (1,1,1,1)
        _Visibility("Visibility", Range(0.001, 10)) = 1
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" 
            "Queue" = "Transparent" 
            "PreviewType" = "Plane"
          //  "LightMode" = "Vertex"
        }
            LOD 100
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                //#pragma alpha:blend
                #pragma target 2.0
                /*#pragma multi_compile_fog
                #define USING_FOG (defined(FOG_LINEAR) || defined(FOG_EXP) || defined(FOG_EXP2))
             */

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                    float3 uv2 : TEXCOORD1;

                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct v2f
                {
                    float4 vertex : SV_POSITION;
                    float2 uv : TEXCOORD0;
                    float3 uv2 : TEXCOORD1;

                    UNITY_VERTEX_OUTPUT_STEREO
                };

                sampler2D _MainTex;
                half4 _MainTex_ST;
                half4 _PlaneColor;
                half _ShortestUVMapping;
                half _Visibility;

                v2f vert(appdata v)
                {
                    v2f o;

                    UNITY_SETUP_INSTANCE_ID(v);
                    UNITY_INITIALIZE_OUTPUT(v2f, o);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.uv2 = v.uv2;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

                //col이 메인 텍스쳐
                    fixed4 col = tex2D(_MainTex, i.uv);
                    //col.a = _Visibility;
                    col = lerp(_PlaneColor, col, col.a);
                    col.a *= 1 - smoothstep(_Visibility, _ShortestUVMapping, i.uv2.y);
                    return col;
                }
                ENDCG
            }
        }
}
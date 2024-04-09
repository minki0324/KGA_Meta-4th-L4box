// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "UI/LoadingShader"
{
    Properties
    {
     _MainTex("Sprite Texture", 2D) = "white" {}   //��� �ؽ�ó  

        _PlaneColor("Plane Color", Color) = (1,1,1,1)   //���İ� ������
        _Visibility("Visibility", Range(0.001, 10)) = 1     //���İ� lerp��

        [Toggle(HORIZONTAL)] _Horizontal("Horizonal", float) = 0    //����or�¿� ����� üũ�ڽ�
        [Toggle(REVERSE)] _ShadeReverse("ShaderReverse", float) = 0    //���� ����� üũ�ڽ�

        _StencilComp("Stencil Comparison", Float) = 8
        _Stencil("Stencil ID", Float) = 0
        _StencilOp("Stencil Operation", Float) = 0
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _StencilReadMask("Stencil Read Mask", Float) = 255

        _ColorMask("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
    }

        SubShader
        {
            Tags
            {
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
                "CanUseSpriteAtlas" = "True"
            }

            Stencil
            {
                Ref[_Stencil]
                Comp[_StencilComp]
                Pass[_StencilOp]
                ReadMask[_StencilReadMask]
                WriteMask[_StencilWriteMask]
            }

            Cull Off
            Lighting Off
            ZWrite Off
            ZTest[unity_GUIZTestMode]
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask[_ColorMask]

            Pass
            {
                Name "Default"
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 2.0

                #include "UnityCG.cginc"
                #include "UnityUI.cginc"

                #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
                #pragma multi_compile_local _ UNITY_UI_ALPHACLIP
                #pragma multi_compile __ DISCARD_BLUE
                #pragma shader_feature HORIZONTAL
                #pragma shader_featrue REVERSE

                struct appdata_t
                {
                    float4 vertex   : POSITION;
                    float4 color    : COLOR;
                    float2 texcoord : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct v2f
                {
                    float4 vertex   : SV_POSITION;
                    fixed4 color : COLOR;
                    float2 texcoord  : TEXCOORD0;
                    float4 worldPosition : TEXCOORD1;
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                sampler2D _MainTex;
                fixed4 _Color;
                fixed4 _TextureSampleAdd;
                float4 _ClipRect;
                float4 _MainTex_ST;
                float4 _PlaneColor;
                float _Visibility;


                v2f vert(appdata_t v)
                {
                    v2f OUT;
                    UNITY_SETUP_INSTANCE_ID(v);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                    OUT.worldPosition = v.vertex;
                    OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                    OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                    OUT.color = v.color * _Color;
                    return OUT;
                }

                fixed4 frag(v2f IN) : SV_Target
                {
                    //half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
                    half4 col = tex2D(_MainTex, IN.texcoord);

                    #ifndef HORIZONTAL
                    //���� üũ�ڽ� Ȱ��ȭ �Ǿ��� ��
                    float alpha = 1 - smoothstep(_Visibility, _Visibility * 0.01, IN.texcoord.x);                   
                    #else
                    //�¿� üũ�ڽ� ��Ȱ��ȭ �Ǿ��� ��
                    float alpha = 1 - smoothstep(_Visibility, _Visibility * 0.01, IN.texcoord.y);
                    #endif

                  


                    col.rgb = lerp(_PlaneColor.rgb, col.rgb, col.a);
                    col.a *= alpha;

                    #ifdef UNITY_UI_CLIP_RECT
                
                    col.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                    #endif

                    #ifdef UNITY_UI_ALPHACLIP
                    clip(col.a - 0.001);
                    #endif

                    return col;
                }
            ENDCG
            }
        }
}
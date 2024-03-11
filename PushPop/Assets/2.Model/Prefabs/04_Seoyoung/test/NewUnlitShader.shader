Shader "UI/asdfasdf"
{
    Properties
    {
        _MainTexture("MainTexture", 2D) = "white" {}
        _Visibility("Visibility", Range(0, 15)) = 5
        [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "UniversalMaterialType" = "Unlit"
            "Queue" = "Transparent"
            "CanUseSpriteAtlas" = "false"
         //   "ShaderGraphShader" = "true"
          //  "ShaderGraphTargetId" = ""         
        }
        Pass
        {
            Name "Sprite Unlit"
            Tags
            {
                "LightMode" = "Universal2D"
            }

        // Render State
        Cull Off

    Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
    ZTest LEqual
    ZWrite Off
  Stencil {
   Ref[_Stencil]
   ReadMask[_StencilReadMask]
   WriteMask[_StencilWriteMask]
   Comp[_StencilComp]
   Pass[_StencilOp]
  }
        // Debug
        // <None>

        // --------------------------------------------------
        // Pass

        HLSLPROGRAM

        // Pragmas
        #pragma target 2.0
    #pragma exclude_renderers d3d11_9x
    #pragma vertex vert
    #pragma fragment frag

        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>

        // Keywords
        #pragma multi_compile_fragment _ DEBUG_DISPLAY
        // GraphKeywords: <None>

        // Defines
        #define _SURFACE_TYPE_TRANSPARENT 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_COLOR
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define VARYINGS_NEED_COLOR
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_SPRITEUNLIT
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

        // Includes
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */

        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

        // --------------------------------------------------
        // Structs and Packing

        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */

        struct Attributes
    {
         float3 positionOS : POSITION;
         float3 normalOS : NORMAL;
         float4 tangentOS : TANGENT;
         float4 uv0 : TEXCOORD0;
         float4 color : COLOR;
        #if UNITY_ANY_INSTANCING_ENABLED
         uint instanceID : INSTANCEID_SEMANTIC;
        #endif
    };
    struct Varyings
    {
         float4 positionCS : SV_POSITION;
         float3 positionWS;
         float4 texCoord0;
         float4 color;
        #if UNITY_ANY_INSTANCING_ENABLED
         uint instanceID : CUSTOM_INSTANCE_ID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
         uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
         uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
         FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
        #endif
    };
    struct SurfaceDescriptionInputs
    {
         float4 uv0;
    };
    struct VertexDescriptionInputs
    {
         float3 ObjectSpaceNormal;
         float3 ObjectSpaceTangent;
         float3 ObjectSpacePosition;
    };
    struct PackedVaryings
    {
         float4 positionCS : SV_POSITION;
         float3 interp0 : INTERP0;
         float4 interp1 : INTERP1;
         float4 interp2 : INTERP2;
        #if UNITY_ANY_INSTANCING_ENABLED
         uint instanceID : CUSTOM_INSTANCE_ID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
         uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
         uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
         FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
        #endif
    };

        PackedVaryings PackVaryings(Varyings input)
    {
        PackedVaryings output;
        ZERO_INITIALIZE(PackedVaryings, output);
        output.positionCS = input.positionCS;
        output.interp0.xyz = input.positionWS;
        output.interp1.xyzw = input.texCoord0;
        output.interp2.xyzw = input.color;
        #if UNITY_ANY_INSTANCING_ENABLED
        output.instanceID = input.instanceID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
        output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
        output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        output.cullFace = input.cullFace;
        #endif
        return output;
    }

    Varyings UnpackVaryings(PackedVaryings input)
    {
        Varyings output;
        output.positionCS = input.positionCS;
        output.positionWS = input.interp0.xyz;
        output.texCoord0 = input.interp1.xyzw;
        output.color = input.interp2.xyzw;
        #if UNITY_ANY_INSTANCING_ENABLED
        output.instanceID = input.instanceID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
        output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
        output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        output.cullFace = input.cullFace;
        #endif
        return output;
    }


    // --------------------------------------------------
    // Graph

    // Graph Properties
    CBUFFER_START(UnityPerMaterial)
float4 _MainTexture_TexelSize;
float _Visibility;
float _Scale;
CBUFFER_END

// Object and Global properties
SAMPLER(SamplerState_Linear_Repeat);
TEXTURE2D(_MainTexture);
SAMPLER(sampler_MainTexture);

// Graph Includes
// GraphIncludes: <None>

// -- Property used by ScenePickingPass
#ifdef SCENEPICKINGPASS
float4 _SelectionID;
#endif

// -- Properties used by SceneSelectionPass
#ifdef SCENESELECTIONPASS
int _ObjectId;
int _PassValue;
#endif

// Graph Functions

void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
{
    RGBA = float4(R, G, B, A);
    RGB = float3(R, G, B);
    RG = float2(R, G);
}

void Unity_Multiply_float_float(float A, float B, out float Out)
{
    Out = A * B;
}

void Unity_Saturate_float(float In, out float Out)
{
    Out = saturate(In);
}

/* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */

// Graph Vertex
struct VertexDescription
{
    float3 Position;
    float3 Normal;
    float3 Tangent;
};

VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
{
    VertexDescription description = (VertexDescription)0;
    description.Position = IN.ObjectSpacePosition;
    description.Normal = IN.ObjectSpaceNormal;
    description.Tangent = IN.ObjectSpaceTangent;
    return description;
}

    #ifdef FEATURES_GRAPH_VERTEX
Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
{
return output;
}
#define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
#endif

// Graph Pixel
struct SurfaceDescription
{
    float3 BaseColor;
    float Alpha;
};

SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
{
    SurfaceDescription surface = (SurfaceDescription)0;
    UnityTexture2D _Property_75a271ee1daa407fb7d97a55a4ec8f82_Out_0 = UnityBuildTexture2DStructNoScale(_MainTexture);
    float4 _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0 = SAMPLE_TEXTURE2D(_Property_75a271ee1daa407fb7d97a55a4ec8f82_Out_0.tex, _Property_75a271ee1daa407fb7d97a55a4ec8f82_Out_0.samplerstate, _Property_75a271ee1daa407fb7d97a55a4ec8f82_Out_0.GetTransformedUV(IN.uv0.xy));
    float _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_R_4 = _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0.r;
    float _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_G_5 = _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0.g;
    float _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_B_6 = _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0.b;
    float _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_A_7 = _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0.a;
    float4 _Combine_884b4244ef3c42dbb5ccb1037182b72e_RGBA_4;
    float3 _Combine_884b4244ef3c42dbb5ccb1037182b72e_RGB_5;
    float2 _Combine_884b4244ef3c42dbb5ccb1037182b72e_RG_6;
    Unity_Combine_float(_SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_R_4, _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_G_5, _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_B_6, 0, _Combine_884b4244ef3c42dbb5ccb1037182b72e_RGBA_4, _Combine_884b4244ef3c42dbb5ccb1037182b72e_RGB_5, _Combine_884b4244ef3c42dbb5ccb1037182b72e_RG_6);
    float4 _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0 = IN.uv0;
    float _Split_3d361d0ee9264237b9d844ea646438b6_R_1 = _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0[0];
    float _Split_3d361d0ee9264237b9d844ea646438b6_G_2 = _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0[1];
    float _Split_3d361d0ee9264237b9d844ea646438b6_B_3 = _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0[2];
    float _Split_3d361d0ee9264237b9d844ea646438b6_A_4 = _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0[3];
    float _Property_45a1d6526c544c82a964ae5c15526f20_Out_0 = _Visibility;
    float _Multiply_2535eb3ce8bf4e669bc053aa454136a8_Out_2;
    Unity_Multiply_float_float(_Split_3d361d0ee9264237b9d844ea646438b6_G_2, _Property_45a1d6526c544c82a964ae5c15526f20_Out_0, _Multiply_2535eb3ce8bf4e669bc053aa454136a8_Out_2);
    float _Multiply_b7f0dd2163f347aa9b8ca222ac71344d_Out_2;
    Unity_Multiply_float_float(_SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_A_7, _Multiply_2535eb3ce8bf4e669bc053aa454136a8_Out_2, _Multiply_b7f0dd2163f347aa9b8ca222ac71344d_Out_2);
    float _Saturate_f2bbe44241a44dcab79f72ef306e5b5e_Out_1;
    Unity_Saturate_float(_Multiply_b7f0dd2163f347aa9b8ca222ac71344d_Out_2, _Saturate_f2bbe44241a44dcab79f72ef306e5b5e_Out_1);
    surface.BaseColor = _Combine_884b4244ef3c42dbb5ccb1037182b72e_RGB_5;
    surface.Alpha = _Saturate_f2bbe44241a44dcab79f72ef306e5b5e_Out_1;
    return surface;
}

// --------------------------------------------------
// Build Graph Inputs

VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
{
    VertexDescriptionInputs output;
    ZERO_INITIALIZE(VertexDescriptionInputs, output);

    output.ObjectSpaceNormal = input.normalOS;
    output.ObjectSpaceTangent = input.tangentOS.xyz;
    output.ObjectSpacePosition = input.positionOS;

    return output;
}
    SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
{
    SurfaceDescriptionInputs output;
    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);







    output.uv0 = input.texCoord0;
#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN                output.FaceSign =                                   IS_FRONT_VFACE(input.cullFace, true, false);
#else
#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
#endif
#undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

    return output;
}

    // --------------------------------------------------
    // Main

    #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
#include "Packages/com.unity.render-pipelines.universal/Editor/2D/ShaderGraph/Includes/SpriteUnlitPass.hlsl"

    ENDHLSL
}
Pass
{
    Name "SceneSelectionPass"
    Tags
    {
        "LightMode" = "SceneSelectionPass"
    }

        // Render State
        Cull Off

        // Debug
        // <None>

        // --------------------------------------------------
        // Pass

        HLSLPROGRAM

        // Pragmas
        #pragma target 2.0
    #pragma exclude_renderers d3d11_9x
    #pragma vertex vert
    #pragma fragment frag

        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>

        // Keywords
        // PassKeywords: <None>
        // GraphKeywords: <None>

        // Defines
        #define _SURFACE_TYPE_TRANSPARENT 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHONLY
    #define SCENESELECTIONPASS 1

        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

        // Includes
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */

        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

        // --------------------------------------------------
        // Structs and Packing

        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */

        struct Attributes
    {
         float3 positionOS : POSITION;
         float3 normalOS : NORMAL;
         float4 tangentOS : TANGENT;
         float4 uv0 : TEXCOORD0;
        #if UNITY_ANY_INSTANCING_ENABLED
         uint instanceID : INSTANCEID_SEMANTIC;
        #endif
    };
    struct Varyings
    {
         float4 positionCS : SV_POSITION;
         float4 texCoord0;
        #if UNITY_ANY_INSTANCING_ENABLED
         uint instanceID : CUSTOM_INSTANCE_ID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
         uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
         uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
         FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
        #endif
    };
    struct SurfaceDescriptionInputs
    {
         float4 uv0;
    };
    struct VertexDescriptionInputs
    {
         float3 ObjectSpaceNormal;
         float3 ObjectSpaceTangent;
         float3 ObjectSpacePosition;
    };
    struct PackedVaryings
    {
         float4 positionCS : SV_POSITION;
         float4 interp0 : INTERP0;
        #if UNITY_ANY_INSTANCING_ENABLED
         uint instanceID : CUSTOM_INSTANCE_ID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
         uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
         uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
         FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
        #endif
    };

        PackedVaryings PackVaryings(Varyings input)
    {
        PackedVaryings output;
        ZERO_INITIALIZE(PackedVaryings, output);
        output.positionCS = input.positionCS;
        output.interp0.xyzw = input.texCoord0;
        #if UNITY_ANY_INSTANCING_ENABLED
        output.instanceID = input.instanceID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
        output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
        output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        output.cullFace = input.cullFace;
        #endif
        return output;
    }

    Varyings UnpackVaryings(PackedVaryings input)
    {
        Varyings output;
        output.positionCS = input.positionCS;
        output.texCoord0 = input.interp0.xyzw;
        #if UNITY_ANY_INSTANCING_ENABLED
        output.instanceID = input.instanceID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
        output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
        output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        output.cullFace = input.cullFace;
        #endif
        return output;
    }


    // --------------------------------------------------
    // Graph

    // Graph Properties
    CBUFFER_START(UnityPerMaterial)
float4 _MainTexture_TexelSize;
float _Visibility;
float _Scale;
CBUFFER_END

// Object and Global properties
SAMPLER(SamplerState_Linear_Repeat);
TEXTURE2D(_MainTexture);
SAMPLER(sampler_MainTexture);

// Graph Includes
// GraphIncludes: <None>

// -- Property used by ScenePickingPass
#ifdef SCENEPICKINGPASS
float4 _SelectionID;
#endif

// -- Properties used by SceneSelectionPass
#ifdef SCENESELECTIONPASS
int _ObjectId;
int _PassValue;
#endif

// Graph Functions

void Unity_Multiply_float_float(float A, float B, out float Out)
{
    Out = A * B;
}

void Unity_Saturate_float(float In, out float Out)
{
    Out = saturate(In);
}

/* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */

// Graph Vertex
struct VertexDescription
{
    float3 Position;
    float3 Normal;
    float3 Tangent;
};

VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
{
    VertexDescription description = (VertexDescription)0;
    description.Position = IN.ObjectSpacePosition;
    description.Normal = IN.ObjectSpaceNormal;
    description.Tangent = IN.ObjectSpaceTangent;
    return description;
}

    #ifdef FEATURES_GRAPH_VERTEX
Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
{
return output;
}
#define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
#endif

// Graph Pixel
struct SurfaceDescription
{
    float Alpha;
};

SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
{
    SurfaceDescription surface = (SurfaceDescription)0;
    UnityTexture2D _Property_75a271ee1daa407fb7d97a55a4ec8f82_Out_0 = UnityBuildTexture2DStructNoScale(_MainTexture);
    float4 _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0 = SAMPLE_TEXTURE2D(_Property_75a271ee1daa407fb7d97a55a4ec8f82_Out_0.tex, _Property_75a271ee1daa407fb7d97a55a4ec8f82_Out_0.samplerstate, _Property_75a271ee1daa407fb7d97a55a4ec8f82_Out_0.GetTransformedUV(IN.uv0.xy));
    float _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_R_4 = _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0.r;
    float _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_G_5 = _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0.g;
    float _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_B_6 = _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0.b;
    float _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_A_7 = _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0.a;
    float4 _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0 = IN.uv0;
    float _Split_3d361d0ee9264237b9d844ea646438b6_R_1 = _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0[0];
    float _Split_3d361d0ee9264237b9d844ea646438b6_G_2 = _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0[1];
    float _Split_3d361d0ee9264237b9d844ea646438b6_B_3 = _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0[2];
    float _Split_3d361d0ee9264237b9d844ea646438b6_A_4 = _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0[3];
    float _Property_45a1d6526c544c82a964ae5c15526f20_Out_0 = _Visibility;
    float _Multiply_2535eb3ce8bf4e669bc053aa454136a8_Out_2;
    Unity_Multiply_float_float(_Split_3d361d0ee9264237b9d844ea646438b6_G_2, _Property_45a1d6526c544c82a964ae5c15526f20_Out_0, _Multiply_2535eb3ce8bf4e669bc053aa454136a8_Out_2);
    float _Multiply_b7f0dd2163f347aa9b8ca222ac71344d_Out_2;
    Unity_Multiply_float_float(_SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_A_7, _Multiply_2535eb3ce8bf4e669bc053aa454136a8_Out_2, _Multiply_b7f0dd2163f347aa9b8ca222ac71344d_Out_2);
    float _Saturate_f2bbe44241a44dcab79f72ef306e5b5e_Out_1;
    Unity_Saturate_float(_Multiply_b7f0dd2163f347aa9b8ca222ac71344d_Out_2, _Saturate_f2bbe44241a44dcab79f72ef306e5b5e_Out_1);
    surface.Alpha = _Saturate_f2bbe44241a44dcab79f72ef306e5b5e_Out_1;
    return surface;
}

// --------------------------------------------------
// Build Graph Inputs

VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
{
    VertexDescriptionInputs output;
    ZERO_INITIALIZE(VertexDescriptionInputs, output);

    output.ObjectSpaceNormal = input.normalOS;
    output.ObjectSpaceTangent = input.tangentOS.xyz;
    output.ObjectSpacePosition = input.positionOS;

    return output;
}
    SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
{
    SurfaceDescriptionInputs output;
    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);







    output.uv0 = input.texCoord0;
#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN                output.FaceSign =                                   IS_FRONT_VFACE(input.cullFace, true, false);
#else
#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
#endif
#undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

    return output;
}

    // --------------------------------------------------
    // Main

    #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/SelectionPickingPass.hlsl"

    ENDHLSL
}
Pass
{
    Name "ScenePickingPass"
    Tags
    {
        "LightMode" = "Picking"
    }

        // Render State
        Cull Off

        // Debug
        // <None>

        // --------------------------------------------------
        // Pass

        HLSLPROGRAM

        // Pragmas
        #pragma target 2.0
    #pragma exclude_renderers d3d11_9x
    #pragma vertex vert
    #pragma fragment frag

        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>

        // Keywords
        // PassKeywords: <None>
        // GraphKeywords: <None>

        // Defines
        #define _SURFACE_TYPE_TRANSPARENT 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHONLY
    #define SCENEPICKINGPASS 1

        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

        // Includes
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */

        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

        // --------------------------------------------------
        // Structs and Packing

        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */

        struct Attributes
    {
         float3 positionOS : POSITION;
         float3 normalOS : NORMAL;
         float4 tangentOS : TANGENT;
         float4 uv0 : TEXCOORD0;
        #if UNITY_ANY_INSTANCING_ENABLED
         uint instanceID : INSTANCEID_SEMANTIC;
        #endif
    };
    struct Varyings
    {
         float4 positionCS : SV_POSITION;
         float4 texCoord0;
        #if UNITY_ANY_INSTANCING_ENABLED
         uint instanceID : CUSTOM_INSTANCE_ID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
         uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
         uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
         FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
        #endif
    };
    struct SurfaceDescriptionInputs
    {
         float4 uv0;
    };
    struct VertexDescriptionInputs
    {
         float3 ObjectSpaceNormal;
         float3 ObjectSpaceTangent;
         float3 ObjectSpacePosition;
    };
    struct PackedVaryings
    {
         float4 positionCS : SV_POSITION;
         float4 interp0 : INTERP0;
        #if UNITY_ANY_INSTANCING_ENABLED
         uint instanceID : CUSTOM_INSTANCE_ID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
         uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
         uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
         FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
        #endif
    };

        PackedVaryings PackVaryings(Varyings input)
    {
        PackedVaryings output;
        ZERO_INITIALIZE(PackedVaryings, output);
        output.positionCS = input.positionCS;
        output.interp0.xyzw = input.texCoord0;
        #if UNITY_ANY_INSTANCING_ENABLED
        output.instanceID = input.instanceID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
        output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
        output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        output.cullFace = input.cullFace;
        #endif
        return output;
    }

    Varyings UnpackVaryings(PackedVaryings input)
    {
        Varyings output;
        output.positionCS = input.positionCS;
        output.texCoord0 = input.interp0.xyzw;
        #if UNITY_ANY_INSTANCING_ENABLED
        output.instanceID = input.instanceID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
        output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
        output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        output.cullFace = input.cullFace;
        #endif
        return output;
    }


    // --------------------------------------------------
    // Graph

    // Graph Properties
    CBUFFER_START(UnityPerMaterial)
float4 _MainTexture_TexelSize;
float _Visibility;
float _Scale;
CBUFFER_END

// Object and Global properties
SAMPLER(SamplerState_Linear_Repeat);
TEXTURE2D(_MainTexture);
SAMPLER(sampler_MainTexture);

// Graph Includes
// GraphIncludes: <None>

// -- Property used by ScenePickingPass
#ifdef SCENEPICKINGPASS
float4 _SelectionID;
#endif

// -- Properties used by SceneSelectionPass
#ifdef SCENESELECTIONPASS
int _ObjectId;
int _PassValue;
#endif

// Graph Functions

void Unity_Multiply_float_float(float A, float B, out float Out)
{
    Out = A * B;
}

void Unity_Saturate_float(float In, out float Out)
{
    Out = saturate(In);
}

/* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */

// Graph Vertex
struct VertexDescription
{
    float3 Position;
    float3 Normal;
    float3 Tangent;
};

VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
{
    VertexDescription description = (VertexDescription)0;
    description.Position = IN.ObjectSpacePosition;
    description.Normal = IN.ObjectSpaceNormal;
    description.Tangent = IN.ObjectSpaceTangent;
    return description;
}

    #ifdef FEATURES_GRAPH_VERTEX
Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
{
return output;
}
#define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
#endif

// Graph Pixel
struct SurfaceDescription
{
    float Alpha;
};

SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
{
    SurfaceDescription surface = (SurfaceDescription)0;
    UnityTexture2D _Property_75a271ee1daa407fb7d97a55a4ec8f82_Out_0 = UnityBuildTexture2DStructNoScale(_MainTexture);
    float4 _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0 = SAMPLE_TEXTURE2D(_Property_75a271ee1daa407fb7d97a55a4ec8f82_Out_0.tex, _Property_75a271ee1daa407fb7d97a55a4ec8f82_Out_0.samplerstate, _Property_75a271ee1daa407fb7d97a55a4ec8f82_Out_0.GetTransformedUV(IN.uv0.xy));
    float _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_R_4 = _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0.r;
    float _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_G_5 = _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0.g;
    float _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_B_6 = _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0.b;
    float _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_A_7 = _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0.a;
    float4 _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0 = IN.uv0;
    float _Split_3d361d0ee9264237b9d844ea646438b6_R_1 = _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0[0];
    float _Split_3d361d0ee9264237b9d844ea646438b6_G_2 = _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0[1];
    float _Split_3d361d0ee9264237b9d844ea646438b6_B_3 = _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0[2];
    float _Split_3d361d0ee9264237b9d844ea646438b6_A_4 = _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0[3];
    float _Property_45a1d6526c544c82a964ae5c15526f20_Out_0 = _Visibility;
    float _Multiply_2535eb3ce8bf4e669bc053aa454136a8_Out_2;
    Unity_Multiply_float_float(_Split_3d361d0ee9264237b9d844ea646438b6_G_2, _Property_45a1d6526c544c82a964ae5c15526f20_Out_0, _Multiply_2535eb3ce8bf4e669bc053aa454136a8_Out_2);
    float _Multiply_b7f0dd2163f347aa9b8ca222ac71344d_Out_2;
    Unity_Multiply_float_float(_SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_A_7, _Multiply_2535eb3ce8bf4e669bc053aa454136a8_Out_2, _Multiply_b7f0dd2163f347aa9b8ca222ac71344d_Out_2);
    float _Saturate_f2bbe44241a44dcab79f72ef306e5b5e_Out_1;
    Unity_Saturate_float(_Multiply_b7f0dd2163f347aa9b8ca222ac71344d_Out_2, _Saturate_f2bbe44241a44dcab79f72ef306e5b5e_Out_1);
    surface.Alpha = _Saturate_f2bbe44241a44dcab79f72ef306e5b5e_Out_1;
    return surface;
}

// --------------------------------------------------
// Build Graph Inputs

VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
{
    VertexDescriptionInputs output;
    ZERO_INITIALIZE(VertexDescriptionInputs, output);

    output.ObjectSpaceNormal = input.normalOS;
    output.ObjectSpaceTangent = input.tangentOS.xyz;
    output.ObjectSpacePosition = input.positionOS;

    return output;
}
    SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
{
    SurfaceDescriptionInputs output;
    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);







    output.uv0 = input.texCoord0;
#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN                output.FaceSign =                                   IS_FRONT_VFACE(input.cullFace, true, false);
#else
#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
#endif
#undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

    return output;
}

    // --------------------------------------------------
    // Main

    #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/SelectionPickingPass.hlsl"

    ENDHLSL
}
Pass
{
    Name "Sprite Unlit"
    Tags
    {
        "LightMode" = "UniversalForward"
    }

        // Render State
        Cull Off
    Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
    ZTest LEqual
    ZWrite Off

        // Debug
        // <None>

        // --------------------------------------------------
        // Pass

        HLSLPROGRAM

        // Pragmas
        #pragma target 2.0
    #pragma exclude_renderers d3d11_9x
    #pragma vertex vert
    #pragma fragment frag

        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>

        // Keywords
        #pragma multi_compile_fragment _ DEBUG_DISPLAY
        // GraphKeywords: <None>

        // Defines
        #define _SURFACE_TYPE_TRANSPARENT 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_COLOR
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define VARYINGS_NEED_COLOR
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_SPRITEFORWARD
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

        // Includes
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */

        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

        // --------------------------------------------------
        // Structs and Packing

        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */

        struct Attributes
    {
         float3 positionOS : POSITION;
         float3 normalOS : NORMAL;
         float4 tangentOS : TANGENT;
         float4 uv0 : TEXCOORD0;
         float4 color : COLOR;
        #if UNITY_ANY_INSTANCING_ENABLED
         uint instanceID : INSTANCEID_SEMANTIC;
        #endif
    };
    struct Varyings
    {
         float4 positionCS : SV_POSITION;
         float3 positionWS;
         float4 texCoord0;
         float4 color;
        #if UNITY_ANY_INSTANCING_ENABLED
         uint instanceID : CUSTOM_INSTANCE_ID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
         uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
         uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
         FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
        #endif
    };
    struct SurfaceDescriptionInputs
    {
         float4 uv0;
    };
    struct VertexDescriptionInputs
    {
         float3 ObjectSpaceNormal;
         float3 ObjectSpaceTangent;
         float3 ObjectSpacePosition;
    };
    struct PackedVaryings
    {
         float4 positionCS : SV_POSITION;
         float3 interp0 : INTERP0;
         float4 interp1 : INTERP1;
         float4 interp2 : INTERP2;
        #if UNITY_ANY_INSTANCING_ENABLED
         uint instanceID : CUSTOM_INSTANCE_ID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
         uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
         uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
         FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
        #endif
    };

        PackedVaryings PackVaryings(Varyings input)
    {
        PackedVaryings output;
        ZERO_INITIALIZE(PackedVaryings, output);
        output.positionCS = input.positionCS;
        output.interp0.xyz = input.positionWS;
        output.interp1.xyzw = input.texCoord0;
        output.interp2.xyzw = input.color;
        #if UNITY_ANY_INSTANCING_ENABLED
        output.instanceID = input.instanceID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
        output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
        output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        output.cullFace = input.cullFace;
        #endif
        return output;
    }

    Varyings UnpackVaryings(PackedVaryings input)
    {
        Varyings output;
        output.positionCS = input.positionCS;
        output.positionWS = input.interp0.xyz;
        output.texCoord0 = input.interp1.xyzw;
        output.color = input.interp2.xyzw;
        #if UNITY_ANY_INSTANCING_ENABLED
        output.instanceID = input.instanceID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
        output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
        output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        output.cullFace = input.cullFace;
        #endif
        return output;
    }


    // --------------------------------------------------
    // Graph

    // Graph Properties
    CBUFFER_START(UnityPerMaterial)
float4 _MainTexture_TexelSize;
float _Visibility;
float _Scale;
CBUFFER_END

// Object and Global properties
SAMPLER(SamplerState_Linear_Repeat);
TEXTURE2D(_MainTexture);
SAMPLER(sampler_MainTexture);

// Graph Includes
// GraphIncludes: <None>

// -- Property used by ScenePickingPass
#ifdef SCENEPICKINGPASS
float4 _SelectionID;
#endif

// -- Properties used by SceneSelectionPass
#ifdef SCENESELECTIONPASS
int _ObjectId;
int _PassValue;
#endif

// Graph Functions

void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
{
    RGBA = float4(R, G, B, A);
    RGB = float3(R, G, B);
    RG = float2(R, G);
}

void Unity_Multiply_float_float(float A, float B, out float Out)
{
    Out = A * B;
}

void Unity_Saturate_float(float In, out float Out)
{
    Out = saturate(In);
}

/* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */

// Graph Vertex
struct VertexDescription
{
    float3 Position;
    float3 Normal;
    float3 Tangent;
};

VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
{
    VertexDescription description = (VertexDescription)0;
    description.Position = IN.ObjectSpacePosition;
    description.Normal = IN.ObjectSpaceNormal;
    description.Tangent = IN.ObjectSpaceTangent;
    return description;
}

    #ifdef FEATURES_GRAPH_VERTEX
Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
{
return output;
}
#define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
#endif

// Graph Pixel
struct SurfaceDescription
{
    float3 BaseColor;
    float Alpha;
};

SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
{
    SurfaceDescription surface = (SurfaceDescription)0;
    UnityTexture2D _Property_75a271ee1daa407fb7d97a55a4ec8f82_Out_0 = UnityBuildTexture2DStructNoScale(_MainTexture);
    float4 _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0 = SAMPLE_TEXTURE2D(_Property_75a271ee1daa407fb7d97a55a4ec8f82_Out_0.tex, _Property_75a271ee1daa407fb7d97a55a4ec8f82_Out_0.samplerstate, _Property_75a271ee1daa407fb7d97a55a4ec8f82_Out_0.GetTransformedUV(IN.uv0.xy));
    float _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_R_4 = _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0.r;
    float _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_G_5 = _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0.g;
    float _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_B_6 = _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0.b;
    float _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_A_7 = _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_RGBA_0.a;
    float4 _Combine_884b4244ef3c42dbb5ccb1037182b72e_RGBA_4;
    float3 _Combine_884b4244ef3c42dbb5ccb1037182b72e_RGB_5;
    float2 _Combine_884b4244ef3c42dbb5ccb1037182b72e_RG_6;
    Unity_Combine_float(_SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_R_4, _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_G_5, _SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_B_6, 0, _Combine_884b4244ef3c42dbb5ccb1037182b72e_RGBA_4, _Combine_884b4244ef3c42dbb5ccb1037182b72e_RGB_5, _Combine_884b4244ef3c42dbb5ccb1037182b72e_RG_6);
    float4 _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0 = IN.uv0;
    float _Split_3d361d0ee9264237b9d844ea646438b6_R_1 = _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0[0];
    float _Split_3d361d0ee9264237b9d844ea646438b6_G_2 = _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0[1];
    float _Split_3d361d0ee9264237b9d844ea646438b6_B_3 = _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0[2];
    float _Split_3d361d0ee9264237b9d844ea646438b6_A_4 = _UV_00a0ef5e51be44e3a4bcf3e216870df9_Out_0[3];
    float _Property_45a1d6526c544c82a964ae5c15526f20_Out_0 = _Visibility;
    float _Multiply_2535eb3ce8bf4e669bc053aa454136a8_Out_2;
    Unity_Multiply_float_float(_Split_3d361d0ee9264237b9d844ea646438b6_G_2, _Property_45a1d6526c544c82a964ae5c15526f20_Out_0, _Multiply_2535eb3ce8bf4e669bc053aa454136a8_Out_2);
    float _Multiply_b7f0dd2163f347aa9b8ca222ac71344d_Out_2;
    Unity_Multiply_float_float(_SampleTexture2D_4898ece5eed845beb00ebee94bc78e81_A_7, _Multiply_2535eb3ce8bf4e669bc053aa454136a8_Out_2, _Multiply_b7f0dd2163f347aa9b8ca222ac71344d_Out_2);
    float _Saturate_f2bbe44241a44dcab79f72ef306e5b5e_Out_1;
    Unity_Saturate_float(_Multiply_b7f0dd2163f347aa9b8ca222ac71344d_Out_2, _Saturate_f2bbe44241a44dcab79f72ef306e5b5e_Out_1);
    surface.BaseColor = _Combine_884b4244ef3c42dbb5ccb1037182b72e_RGB_5;
    surface.Alpha = _Saturate_f2bbe44241a44dcab79f72ef306e5b5e_Out_1;
    return surface;
}

// --------------------------------------------------
// Build Graph Inputs

VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
{
    VertexDescriptionInputs output;
    ZERO_INITIALIZE(VertexDescriptionInputs, output);

    output.ObjectSpaceNormal = input.normalOS;
    output.ObjectSpaceTangent = input.tangentOS.xyz;
    output.ObjectSpacePosition = input.positionOS;

    return output;
}
    SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
{
    SurfaceDescriptionInputs output;
    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);







    output.uv0 = input.texCoord0;
#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN                output.FaceSign =                                   IS_FRONT_VFACE(input.cullFace, true, false);
#else
#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
#endif
#undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

    return output;
}

    // --------------------------------------------------
    // Main

    #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
#include "Packages/com.unity.render-pipelines.universal/Editor/2D/ShaderGraph/Includes/SpriteUnlitPass.hlsl"

    ENDHLSL
}
    }
        CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
        FallBack "Hidden/Shader Graph/FallbackError"
}
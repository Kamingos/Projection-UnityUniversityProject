Shader "Custom/FlipNormalsURP"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        [Toggle(_FLIP_NORMALS)] _FlipNormals ("Flip Normals", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry"
        }

        Cull Off  // Отключаем отсечение граней

        Pass
        {
            Name "Unlit"
            Tags { "LightMode" = "SRPDefaultUnlit" }  // Используем unlit режим

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature_local _FLIP_NORMALS

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float3 normalOS     : NORMAL;
                float2 texcoord     : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float2 uv           : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.texcoord, _MainTex);
                
                // Нормали больше не нужны для unlit шейдера
                #ifdef _FLIP_NORMALS
                    // Инвертируем нормали, но не используем их в освещении
                    float3 normalWS = TransformObjectToWorldNormal(IN.normalOS);
                    normalWS = -normalWS;
                #endif
                
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Просто возвращаем цвет текстуры без освещения
                return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
            }
            ENDHLSL
        }
    }
    
    FallBack "Universal Render Pipeline/Unlit"
}
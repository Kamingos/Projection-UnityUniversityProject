Shader "PostEffect/CRT"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScanlinesWeight ("Scanlines Weight", Range(0, 1)) = 0.5
        _VignetteAmount ("Vignette Amount", Range(0, 1)) = 0.3
    }
    
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
        }
        
        Cull Off
        ZWrite Off
        ZTest Always
        
        Pass
        {
            Name "CRT"
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            float _ScanlinesWeight;
            float _VignetteAmount;
            
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }
            
            half4 frag(Varyings IN) : SV_Target
            {
                // Базовый цвет
                half4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                
                // Простая сканирующая линия
                float scanline = sin(IN.uv.y * 500.0 + _Time.y * 10.0) * 0.1 * _ScanlinesWeight;
                color.rgb += scanline;
                
                // Простая виньетка
                float2 uvCentered = IN.uv * 2.0 - 1.0;
                float vignette = 1.0 - dot(uvCentered, uvCentered) * _VignetteAmount;
                color.rgb *= vignette;
                
                return color;
            }
            ENDHLSL
        }
    }
    
    Fallback Off
}
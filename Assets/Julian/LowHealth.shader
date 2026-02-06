Shader "Custom/DeathVignette"
{
    Properties
    {
        _Intensity ("Intensity", Range(0, 1)) = 0
        _VignetteColor ("Vignette Color", Color) = (0.7, 0, 0, 1)
        _VignetteSize ("Vignette Size", Range(0, 1)) = 0.4
        _VignetteSmoothness ("Smoothness", Range(0.01, 1)) = 0.4
        _NoiseScale ("Noise Scale", Float) = 8
        _NoiseAmount ("Noise Amount", Range(0, 0.5)) = 0.25
        _PulseSpeed ("Pulse Speed", Float) = 3
        _PulseAmount ("Pulse Amount", Range(0, 0.3)) = 0.08
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
        ZWrite Off Cull Off
        
        Pass
        {
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
            
            CBUFFER_START(UnityPerMaterial)
                float _Intensity;
                half4 _VignetteColor;
                float _VignetteSize;
                float _VignetteSmoothness;
                float _NoiseScale;
                float _NoiseAmount;
                float _PulseSpeed;
                float _PulseAmount;
            CBUFFER_END
            
            // Noise function
            float hash(float2 p)
            {
                return frac(sin(dot(p, float2(127.1, 311.7))) * 43758.5453);
            }
            
            float noise(float2 p)
            {
                float2 i = floor(p);
                float2 f = frac(p);
                f = f * f * (3.0 - 2.0 * f);
                
                float a = hash(i);
                float b = hash(i + float2(1.0, 0.0));
                float c = hash(i + float2(0.0, 1.0));
                float d = hash(i + float2(1.0, 1.0));
                
                return lerp(lerp(a, b, f.x), lerp(c, d, f.x), f.y);
            }
            
            half4 frag(Varyings input) : SV_Target
            {
                float2 uv = input.texcoord;
                
                // Sample l'écran
                half4 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, uv);
                
                // Centre les UVs
                float2 centeredUV = uv - 0.5;
                
                // Angle autour du centre (pour le noise radial)
                float angle = atan2(centeredUV.y, centeredUV.x);
                
                // Distance au centre
                float dist = length(centeredUV * float2(1.0, 0.6));
                
                // Noise basé sur l'angle pour créer les pointes irrégulières
                float noiseVal = noise(float2(angle * _NoiseScale, _Time.y * 0.5));
                noiseVal += noise(float2(angle * _NoiseScale * 2.0, _Time.y * 0.3)) * 0.5;
                noiseVal = noiseVal / 1.5;
                
                // Pulse animé
                float pulse = sin(_Time.y * _PulseSpeed) * _PulseAmount * _Intensity;
                
                // Applique le noise à la distance
                float noisyDist = dist - (noiseVal * _NoiseAmount * _Intensity);
                
                // Calcul de la vignette
                float vignetteStart = _VignetteSize - pulse;
                float vignette = smoothstep(vignetteStart, vignetteStart - _VignetteSmoothness, noisyDist);
                vignette = 1.0 - vignette;
                
                // Applique l'intensité
                vignette *= _Intensity;
                
                // Mix avec la couleur de vignette
                color.rgb = lerp(color.rgb, _VignetteColor.rgb, vignette * 0.85);
                
                // Assombrit aussi légèrement
                color.rgb *= lerp(1.0, 0.7, vignette);
                
                return color;
            }
            ENDHLSL
        }
    }
}
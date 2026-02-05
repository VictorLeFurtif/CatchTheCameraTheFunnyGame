Shader "Custom/VortexEffect"
{
    Properties
    {
        _Intensity ("Intensity", Range(0, 1)) = 0
        _VortexStrength ("Vortex Strength", Float) = 15
        _VortexSpeed ("Vortex Speed", Float) = 2
        _Radius ("Radius", Range(0.1, 2)) = 0.8
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
                float _VortexStrength;
                float _VortexSpeed;
                float _Radius;
            CBUFFER_END
            
            float2 VortexDistortion(float2 uv, float strength, float radius)
            {
                float2 centeredUV = uv - 0.5;
                float dist = length(centeredUV);
                float angle = atan2(centeredUV.y, centeredUV.x);
                
                float vortexAmount = saturate(1.0 - dist / radius);
                vortexAmount = vortexAmount * vortexAmount;
                
                angle += vortexAmount * strength;
                
                float2 newUV;
                newUV.x = cos(angle) * dist + 0.5;
                newUV.y = sin(angle) * dist + 0.5;
                
                return newUV;
            }
            
            half4 frag(Varyings input) : SV_Target
            {
                float2 uv = input.texcoord;
                
                float time = _Time.y * _VortexSpeed;
                float animatedStrength = _VortexStrength * sin(time) * _Intensity;
                
                float2 distortedUV = VortexDistortion(uv, animatedStrength, _Radius);
                float2 finalUV = lerp(uv, distortedUV, _Intensity);
                
                half4 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, finalUV);
                
                return color;
            }
            ENDHLSL
        }
    }
}
Shader "Custom/OutlineShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (1,1,1,1)
        _OutlineWidth("Outline Width", Range(0, 0.1)) = 0.01
        _DepthSensitivity("Depth Sensitivity", Range(1, 50)) = 10
        _NormalPower("Normal Power", Range(1, 10)) = 5
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD2;
            };
            
            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
            float4 _OutlineColor;
            float _OutlineWidth;
            float _DepthSensitivity;
            float _NormalPower;
            
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }
            
            float4 frag(v2f i) : SV_Target
            {
                // Основной цвет текстуры
                float4 col = tex2D(_MainTex, i.uv);
                
                // Depth-based outline
                float2 screenUV = i.screenPos.xy / i.screenPos.w;
                float depth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, screenUV));
                
                float depthEdge = 0;
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x == 0 && y == 0) continue;
                        float2 offset = float2(x,y) * _OutlineWidth * (1.0/_ScreenParams.xy);
                        float sampleDepth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, screenUV + offset));
                        depthEdge += abs(sampleDepth - depth);
                    }
                }
                depthEdge = saturate(depthEdge / 8 * _DepthSensitivity);
                
                // Normal-based outline
                float3 viewDir = normalize(UnityWorldSpaceViewDir(i.vertex));
                float normalEdge = pow(1.0 - saturate(dot(viewDir, i.normal)), _NormalPower);
                
                // Комбинируем
                float outline = saturate(depthEdge + normalEdge);
                
                return lerp(col, _OutlineColor, outline);
            }
            ENDCG
        }
    }
}
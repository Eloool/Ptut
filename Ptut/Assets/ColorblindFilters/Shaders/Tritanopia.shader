Shader"Custom/TritanopiaFilter" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float3 Tritanopia(float3 color)
            {
                return float3(
                                0.95 * color.r + 0.05 * color.g + 0.0 * color.b,
                                0.0 * color.r + 0.433 * color.g + 0.567 * color.b,
                                0.0 * color.r + 0.475 * color.g + 0.525 * color.b
                            );
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 color = tex2D(_MainTex, i.uv);
                color.rgb = Tritanopia(color.rgb);
                return color;
            }
            ENDCG
        } // Closing brace for Pass
    } // Closing brace for SubShader
} // Closing brace for Shader

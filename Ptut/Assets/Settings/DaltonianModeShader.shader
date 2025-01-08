Shader "Custom/DaltonianModeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorMode ("Color Mode", Range(0, 2)) = 0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Tags { "LightMode"="ForwardBase" }  // Utilisation du pipeline standard de lumière
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"  // Inclusion des fonctions de base de Unity

            // Définition des structures
            struct appdata_t { float4 vertex : POSITION; float3 normal : NORMAL; float2 uv : TEXCOORD0; };
            struct v2f { float2 uv : TEXCOORD0; float4 vertex : SV_POSITION; float3 normal : TEXCOORD1; };

            // Variables globales
            sampler2D _MainTex;
            float _ColorMode;

            // Fonction de vertex
            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = mul((float3x3)unity_ObjectToWorld, v.normal);  // Calcul des normales en espace monde
                return o;
            }

            // Fonction de fragment (pixel)
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // Matrices pour les différentes formes de daltonisme
                float3x3 protanopia = float3x3(
                    0.56667, 0.43333, 0.0,
                    0.55833, 0.44167, 0.0,
                    0.0,      0.24167, 0.75833
                );

                float3x3 deuteranopia = float3x3(
                    0.625, 0.375, 0.0,
                    0.7,   0.3,   0.0,
                    0.0,   0.3,   0.7
                );

                float3x3 tritanopia = float3x3(
                    0.95,   0.05,  0.0,
                    0.0,    0.433, 0.567,
                    0.0,    0.475, 0.525
                );

                // Sélection du mode de daltonisme
                float3x3 colorMatrix = (int(_ColorMode) == 0) ? protanopia : 
                                        ((int(_ColorMode) == 1) ? deuteranopia : tritanopia);

                // Applique la transformation de couleur
                col.rgb = mul(colorMatrix, col.rgb);

                return col;
            }
            ENDCG
        }
    }
}

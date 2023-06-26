Shader "Custom/BorderOnly_Shader"
{
    Properties
    {
       _Color("ColorBase", color) = (0,0,0,1)
       _ColorBorde("ColorBordes",color) = (1,1,1,1)
    }
   
        // Primer subshader
    SubShader{
       LOD 200

       CGPROGRAM
       // Método para el cálculo de la luz
       #pragma surface surf Standard fullforwardshadows
       #pragma target 3.0

        // Información adicional provista por el juego
    struct Input {
       float3 worldNormal;
       float3 viewDir;
    };

    float4 _Color;
    float4 _ColorBorde;


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float4 mask = dot(IN.viewDir, IN.worldNormal);

            o.Albedo = (_Color * pow(mask, 6)) + (pow(1 - mask, 6) * _ColorBorde);
        }
        ENDCG
    }
    FallBack "Diffuse"
}

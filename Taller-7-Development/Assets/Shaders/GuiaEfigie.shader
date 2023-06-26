Shader "Custom/GuiaEfigie"
{
    Properties
    {
        _Color("Color Base", Color) = (1,1,1,1)
        _Luz("Cantidad de Luz", Range(0,1)) = 0
        _Visibilidad("Multiplicador del Alfa", Range(0,1)) = 1

    }
        SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 200

        ZWrite off
        Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:fade
        #pragma target 3.0

        #include "UnityCG.cginc"
        float4 _Color;
        float _Luz;
        float _Visibilidad;

        struct Input
        {
            float3 worldNormal;
            float3 viewDir;
        };


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float4 mask = dot(IN.viewDir, IN.worldNormal);
            float4 texFinal = (_Color * mask) + ((1 - mask) * _Color);
            o.Albedo = texFinal.rgb;
            o.Alpha = pow(1-mask,3)*_Visibilidad;
            _Luz = pow(1.5*sin( 2* _Time.y),2);
            o.Emission = (1 - mask)*_Luz*_Color;
            //o.Alpha = pow((mask),3).a + (pow(1-mask,3).a *0);
            //o.Alpha = (mask.r + mask.g + mask.b)/3;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

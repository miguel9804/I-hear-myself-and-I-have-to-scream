Shader "Hidden/CameraEffect"
{
    Properties
    {
        _MainTex("Textura", 2D) = "white" {}
        _Factor("Factor", float) = 0.5
    }
        SubShader
        {
            Pass
            {
            CGPROGRAM

            #pragma vertex vert_img
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

            float _Factor;

            float4 frag(v2f_img i) : COLOR{

                float w = _MainTex_TexelSize.x;
                float h = _MainTex_TexelSize.y;
                float thickness = 3;

                float4 main = tex2D(_MainTex, float2(i.uv.x + 0, i.uv.y + 0));
                float4 mainR = tex2D(_MainTex, float2(i.uv.x + thickness * w, i.uv.y + 0));
                float4 mainL = tex2D(_MainTex, float2(i.uv.x - thickness * w, i.uv.y + 0));
                float4 mainU = tex2D(_MainTex, float2(i.uv.x, i.uv.y + thickness * h));
                float4 mainD = tex2D(_MainTex, float2(i.uv.x, i.uv.y - thickness * h));

                float4 tmp = main * 4 + (mainR * -1) + (mainL * -1) + (mainU * -1) + (mainD * -1);
                float4 output = float4(0, 0, 0, 1);
                output = tmp;

                    return output;
                    /*float4 c1 = tex2D(_MainTex, i.uv);
                    float4 c2 = (c1) * (1+_Factor);
                    return c2;
                    */
                }
                    ENDCG
                }

        }

            FallBack "Diffuse"
}

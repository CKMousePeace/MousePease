

Shader "Unlit/Cheese"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _AlphaColor("Alpha" , Range(0 , 1)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"  "RenderType" = "Transparent" "IgnoreProjector" = "True" }
        LOD 100

        Pass
        {

            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha


            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;                                
            };

            sampler2D _MainTex;
            float _AlphaColor;
            v2f vert (appdata v)
            {
                v2f o;                                                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {                
                fixed4 col = tex2D(_MainTex, i.uv);  
                col.a = _AlphaColor;
                return col;
            }
            ENDCG
        }
    }
}

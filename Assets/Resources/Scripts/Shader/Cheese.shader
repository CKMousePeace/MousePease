// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Cheese"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Intensity("Intensity" , Range(0.001, 10)) = 0.5
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
                float2 uv : TEXCOORD0;                
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD1;       
                float3 PlayerPos : TEXCOORD2;
                
                

            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            uniform float4 _PlayerPos;
            float _Intensity;

            v2f vert (appdata v)
            {
                v2f o;                                
                o.worldPos = UnityObjectToClipPos( v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);            

                _PlayerPos = mul(UNITY_MATRIX_VP,_PlayerPos);
                o.PlayerPos = _PlayerPos.xyz;
                

                return o;
            }



            fixed4 frag (v2f i) : SV_Target
            {                
                fixed4 col = tex2D(_MainTex, i.uv);    
               
                float3 PlayerPos = i.PlayerPos;                
                col.a = saturate(distance (i.worldPos.xyz, PlayerPos.xyz) / _Intensity);               
                
                return col;
            }
            ENDCG
        }
    }
}

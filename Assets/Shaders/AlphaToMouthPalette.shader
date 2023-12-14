Shader "Unlit/AlphaToMouthPalette"
{
    Properties
    {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
        [NoScaleOffset] _PaletteTex ("Palette", 2D) = "white" {}
        _IndexInt ("Index", Integer) = 0
        [HideInInspector]_StencilComp ("Stencil Comparison", Float) = 8
		[HideInInspector]_Stencil ("Stencil ID", Float) = 0
		[HideInInspector]_StencilOp ("Stencil Operation", Float) = 0
		[HideInInspector]_StencilWriteMask ("Stencil Write Mask", Float) = 255
		[HideInInspector]_StencilReadMask ("Stencil Read Mask", Float) = 255
		[HideInInspector]_ColorMask ("Color Mask", Float) = 15
    }
    SubShader
    {
        Tags { "Queue"="AlphaTest" "RenderType"="TransparentCutout" "PreviewType"="Plane" }
        LOD 100

        Stencil
        {
            Ref[_Stencil]
			Comp[_StencilComp]
			Pass [_StencilOp] 
            ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
        }
 		ColorMask [_ColorMask]
 
        Pass
        {
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
            };
 
            sampler2D _MainTex;
            sampler2D _PaletteTex;
            float4 _PaletteTex_TexelSize;
            int _IndexInt;
         
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
         
            fixed4 frag(v2f i) : SV_Target
            {
                fixed alphaIndex = tex2D(_MainTex, i.uv).a;
                float paletteU = (((alphaIndex < 0.1) ? 0 : (alphaIndex < 0.2) ? 2 : (alphaIndex < 0.3) ? 3 : (alphaIndex < 0.8) ? 2 : 1) + 0.5) * _PaletteTex_TexelSize.x;
                float paletteV = (((alphaIndex < 0.1) ? 0 : (alphaIndex < 0.2) ? 0 : (alphaIndex < 0.3) ? 0 : (alphaIndex < 0.8) ? _IndexInt + 1 : 0) + 0.5) * _PaletteTex_TexelSize.y;
                fixed4 col = tex2D(_PaletteTex, float2(paletteU, paletteV));
    
                clip(col.a-0.05);
                return col;
            }
            ENDCG
        }
    }
}
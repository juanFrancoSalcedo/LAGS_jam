// Made with Amplify Shader Editor v1.9.3.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Ifooboo/Object Fade"
{
	Properties
	{
		_MainTex("Albedo", 2D) = "white" {}
		[NoScaleOffset]_BumpMap("Normal Map", 2D) = "bump" {}
		[NoScaleOffset]_MetallicGlossMap("Metallic Map", 2D) = "white" {}
		[NoScaleOffset]_OcclusionMap("Occlusion Map", 2D) = "white" {}
		[NoScaleOffset]_DetailMask("Detail Mask", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_BumpScale("Normal Scale", Float) = 1
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_GlossMapScale("Smoothness", Range( 0 , 1)) = 0.5
		_OcclusionStrength("Occlusion", Range( 0 , 1)) = 1
		[NoScaleOffset]_EmissionMap("Emission", 2D) = "white" {}
		[HDR]_EmissionColor("Emission Color", Color) = (0,0,0,1)
		_DetailAlbedoMap("Detail Albedo x2", 2D) = "gray" {}
		[NoScaleOffset]_DetailNormalMap("Detail Normal", 2D) = "bump" {}
		_DetailNormalMapScale("Detail Normal Scale", Float) = 1
		_MaskClipValue("Mask Clip Value", Float) = 0
		_Fade("Fade", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPosition;
		};

		uniform sampler2D _BumpMap;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float _BumpScale;
		uniform sampler2D _DetailNormalMap;
		uniform sampler2D _DetailAlbedoMap;
		uniform float4 _DetailAlbedoMap_ST;
		uniform float _DetailNormalMapScale;
		uniform sampler2D _DetailMask;
		uniform float4 _Color;
		uniform sampler2D _EmissionMap;
		uniform float4 _EmissionColor;
		uniform sampler2D _MetallicGlossMap;
		uniform float _Metallic;
		uniform float _GlossMapScale;
		uniform sampler2D _OcclusionMap;
		uniform float _OcclusionStrength;
		uniform float _Fade;
		uniform float _MaskClipValue;


		inline float Dither4x4Bayer( int x, int y )
		{
			const float dither[ 16 ] = {
				 1,  9,  3, 11,
				13,  5, 15,  7,
				 4, 12,  2, 10,
				16,  8, 14,  6 };
			int r = y * 4 + x;
			return dither[r] / 16; // same # of instructions as pre-dividing due to compiler magic
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float4 ase_screenPos = ComputeScreenPos( UnityObjectToClipPos( v.vertex ) );
			o.screenPosition = ase_screenPos;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float3 tex2DNode37_g80 = UnpackScaleNormal( tex2D( _BumpMap, uv_MainTex ), _BumpScale );
			float2 uv_DetailAlbedoMap = i.uv_texcoord * _DetailAlbedoMap_ST.xy + _DetailAlbedoMap_ST.zw;
			float4 tex2DNode42_g80 = tex2D( _DetailMask, uv_MainTex );
			float3 lerpResult60_g80 = lerp( tex2DNode37_g80 , BlendNormals( tex2DNode37_g80 , UnpackScaleNormal( tex2D( _DetailNormalMap, uv_DetailAlbedoMap ), _DetailNormalMapScale ) ) , tex2DNode42_g80.a);
			o.Normal = lerpResult60_g80;
			o.Albedo = ( ( tex2D( _MainTex, uv_MainTex ) * _Color ) * float4( ( ( ( (tex2D( _DetailAlbedoMap, uv_DetailAlbedoMap )).rgb * (unity_ColorSpaceDouble).rgb ) * tex2DNode42_g80.a ) + ( 1.0 - tex2DNode42_g80.a ) ) , 0.0 ) ).rgb;
			o.Emission = ( tex2D( _EmissionMap, uv_MainTex ) * _EmissionColor ).rgb;
			float4 tex2DNode2_g80 = tex2D( _MetallicGlossMap, uv_MainTex );
			o.Metallic = ( tex2DNode2_g80.r * _Metallic );
			o.Smoothness = ( tex2DNode2_g80.a * _GlossMapScale );
			float lerpResult13_g80 = lerp( tex2D( _OcclusionMap, uv_MainTex ).g , 1.0 , ( 1.0 - _OcclusionStrength ));
			o.Occlusion = lerpResult13_g80;
			o.Alpha = 1;
			float4 ase_screenPos = i.screenPosition;
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float2 clipScreen3_g82 = ase_screenPosNorm.xy * _ScreenParams.xy;
			float dither3_g82 = Dither4x4Bayer( fmod(clipScreen3_g82.x, 4), fmod(clipScreen3_g82.y, 4) );
			clip( dither3_g82 - ( 0.95 * ( 0.06 + _Fade ) ));
			clip( 0.0 - _MaskClipValue );
		}

		ENDCG
	}
	Fallback "Diffuse"
	// CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19303
Node;AmplifyShaderEditor.FunctionNode;82;-256,0;Inherit;False;Standard PBR;0;;80;d90e2c9dd783d0f429edbdbfd2d091f0;0;0;6;COLOR;0;FLOAT3;39;COLOR;28;FLOAT;17;FLOAT;18;FLOAT;20
Node;AmplifyShaderEditor.RangedFloatNode;84;-256,352;Inherit;False;Constant;_MaskClipValue;Mask Clip Value;16;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;85;-256,240;Inherit;False;Dither Fade;16;;82;bda778d9b40dd224194c62ad9744b15f;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-16,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Ifooboo/Object Fade;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Custom;0;True;True;0;True;TransparentCutout;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;True;_MaskClipValue;0;0;0;False;0.1;False;;0;False;;False;17;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;82;0
WireConnection;0;1;82;39
WireConnection;0;2;82;28
WireConnection;0;3;82;17
WireConnection;0;4;82;18
WireConnection;0;5;82;20
WireConnection;0;10;85;0
ASEEND*/
//CHKSM=C54DC649E0F6712D967075AD571E81C100A496DA
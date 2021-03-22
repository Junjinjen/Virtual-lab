#ifndef SIGNATURE_H
#define SIGNATURE_H

struct VertexShaderInput
{
	float4 position : POSITION;
	float4 normal : NORMAL;
	float4 color : COLOR;
	float2 textureCoordinate : TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 screenSpacePosition : SV_Position;
	float4 worldSpacePosition : POSITION0;
	float3 normal : POSITION1;
	float4 color : COLOR;
	float4 textureCoordinate : TEXCOORD0;
};

#endif // !SIGNATURE_H

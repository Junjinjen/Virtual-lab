#include "Headers/Vertex.h"
#include "Headers/Pixel.h"
//#include "Headers/Lightning.h"

VertexShaderOutput VS(VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;
	output.screenSpacePosition = mul(input.position, worldViewProjectionMatrix);
	output.color = input.color;

	return output;
}

float4 PS(VertexShaderOutput input) : SV_Target
{
	float mipLevel = meshTexture.CalculateLevelOfDetail(textureSampler, input.textureCoordinate);
	float4 texColor = meshTexture.SampleLevel(textureSampler, input.textureCoordinate, mipLevel);
	return texColor;
}
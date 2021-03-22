#include "Headers/Vertex.h"
#include "Headers/Pixel.h"
#include "Headers/Lightning.h"

VertexShaderOutput VS(VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;
	output.screenSpacePosition = mul(input.position, worldViewProjectionMatrix);
	output.color = input.color;

	return output;
}

float4 PS(VertexShaderOutput input) : SV_Target
{
	return input.color;
}
#include "Headers/Vertex.h"
#include "Headers/Pixel.h"
#include "Headers/Lightning.h"

VertexShaderOutput VS(VertexShaderInput input)
{
	PS_IN output = (PS_IN)0;

	output.pos = mul(input.pos, worldViewProj);
	output.col = input.col;

	return output;
}

float4 PS(PS_IN input) : SV_Target
{
	return input.col;
}
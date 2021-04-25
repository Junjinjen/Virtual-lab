#include "Headers/Vertex.h"
#include "Headers/Pixel.h"
#include "Headers/Lightning.h"

VertexShaderOutput VS(VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;
	output.screenSpacePosition = mul(input.position, worldViewProjectionMatrix);
	output.color = input.color;
	output.textureCoordinate = input.textureCoordinate;

	return output;
}

float4 PS(VertexShaderOutput input) : SV_Target
{
    if (isTexturePresent)
    {
		float mipLevel = meshTexture.CalculateLevelOfDetail(textureSampler, input.textureCoordinate);
		float4 texColor = meshTexture.SampleLevel(textureSampler, input.textureCoordinate, mipLevel);
        input.color = texColor;
    }
	
    input.color.xyz *= diffusionCoefficient;
	
    return input.color;
}
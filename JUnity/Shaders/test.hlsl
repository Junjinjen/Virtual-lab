#include "Headers/Vertex.h"
#include "Headers/Pixel.h"
#include "Headers/Lightning.h"

VertexShaderOutput VS(VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;
	output.screenSpacePosition = mul(input.position, worldViewProjectionMatrix);
    output.worldSpacePosition = mul(input.position, worldMatrix);
    output.normal = normalize(mul(input.normal.xyz, (float3x3) inverseWorldMatrix));
	output.color = input.color;
	output.textureCoordinate = input.textureCoordinate;

	return output;
}

struct lightParts
{
    float3 diffusePart;
    float3 specularPart;
};

float3 computeDiffusePart(float3 color, float3 toLight, float3 normal)
{
    float normalDotLight = max(0.00f, dot(normal, toLight));
        
    return color * normalDotLight;
}

float3 computeSpecularPart(float3 color, float3 toLight, float3 normal, float3 toEye, float specularPower)
{
    float3 h = (toLight + toEye) / abs(toLight + toEye);
    float3 hn = dot(h, normal);
    float3 hn2 = normalize(hn) * normalize(hn);
        
    return color * exp(-1.7f * (1.0f - hn2) / hn2);
}

float computeAttenuation(float3 attenuation, float distance)
{
    return 1.0f / (attenuation.x + attenuation.y * distance + attenuation.z * distance * distance);
}

float4 PS(VertexShaderOutput input) : SV_Target
{
    float4 materialColor = 0;
    
    if (isTexturePresent)
    {
		float mipLevel = meshTexture.CalculateLevelOfDetail(textureSampler, input.textureCoordinate);
        materialColor = meshTexture.SampleLevel(textureSampler, input.textureCoordinate, mipLevel) * input.color;
    }
    else
    {
        materialColor = input.color;
    }
    
    input.color = (emissivityCoefficient + ambientCoefficient * float4(globalAmbient, 1)) * materialColor;
    
    float3 diffuseColorPart = 0;
    float3 specularColorPart = 0;
	
    for (int i = 0; i < activeDirectionalLights; i++)
    {
        DirectionalLight light = directionalLights[i];
        float3 toLight = -light.direction;
        float3 toEye = normalize(cameraPosition - input.worldSpacePosition.xyz);
        float3 normal = input.normal.xyz;
        
        diffuseColorPart += computeDiffusePart(light.color, toLight, normal);
        specularColorPart += computeSpecularPart(light.color, toLight, normal, toEye, specularPower);
    }
    
    for (int j = 0; j < activePointLights; j++)
    {
        PointLight light = pointLights[j];
        float3 toLight = light.position - input.worldSpacePosition.xyz;
        float3 toEye = normalize(cameraPosition - input.worldSpacePosition.xyz);
        float3 normal = input.normal.xyz;
        float attenuation = computeAttenuation(light.attenuation, length(toLight));
        toLight = normalize(toLight);
        
        diffuseColorPart += computeDiffusePart(light.color, toLight, normal) * attenuation;
        specularColorPart += computeSpecularPart(light.color, toLight, normal, toEye, specularPower) * attenuation;
    }
    
    for (int k = 0; k < activeSpotLights; k++)
    {
        SpotLight light = spotLights[k];
        float3 toLight = normalize(light.position - input.worldSpacePosition.xyz);
        
        float minCos = cos(light.angle);
        float maxCos = (minCos + 1.0f) / 2.0f;
        float cosAngle = dot(light.direction, -toLight);
        
        float spotIntensity = smoothstep(minCos, maxCos, cosAngle);
        
        toLight = light.position - input.worldSpacePosition.xyz;
        float3 toEye = normalize(cameraPosition - input.worldSpacePosition.xyz);
        float3 normal = input.normal.xyz;
        float attenuation = computeAttenuation(light.attenuation, length(toLight));
        toLight = normalize(toLight);
        
        diffuseColorPart += computeDiffusePart(light.color, toLight, normal) * attenuation * spotIntensity;
        specularColorPart += computeSpecularPart(light.color, toLight, normal, toEye, specularPower) * attenuation * spotIntensity;
    }
    
    float3 light = diffuseColorPart * diffusionCoefficient.xyz;
    light += specularColorPart * specularCoefficient.xyz;
    input.color.xyz += light * materialColor.xyz;
    
    return input.color;
}
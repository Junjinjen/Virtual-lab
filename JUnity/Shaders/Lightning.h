﻿#define MAX_LIGHTS_PER_TYPE = 8

struct DirectionalLight
{
	float3 direction;
	float3 color;
};

struct PointLight
{
	float3 position;
	float3 color;
	float3 attenuation;
};

struct SpotLight
{
	float3 position;
	float3 direction;
	float angle;
	float3 color;
	float3 attenuation;
};

cbuffer LightContainer : register(b0)
{
	float3 cameraPosition;
	int activeDirectionalLights;
	int activePointLights;
	int activeSpotLights;
	float3 globalAmbient;
	SpotLight spotLights[MAX_LIGHTS_PER_TYPE];
	PointLight pointLights[MAX_LIGHTS_PER_TYPE];
	DirectionalLight directionalLights[MAX_LIGHTS_PER_TYPE];
}

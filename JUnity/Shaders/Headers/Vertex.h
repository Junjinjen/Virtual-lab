#include "Signature.h"

cbuffer ViewProjectionMatrices : register(b0)
{
	float4x4 viewMatrix;
	float4x4 projectionMatrix;
}

cbuffer WorldMatrixBuffer : register(b1)
{
	float4x4 worldMatrix;
}

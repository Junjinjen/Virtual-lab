#ifndef VERTEX_H
#define VERTEX_H

#include "Signature.h"

cbuffer MeshMatrices : register(b2)
{
	float4x4 worldViewProjectionMatrix;
	float4x4 worldMatrix;
	float4x4 inverseWorldMatrix;
}

#endif // !VERTEX_H

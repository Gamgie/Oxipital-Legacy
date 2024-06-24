/*
Vector AB is axis
P is current point.
I the point P projected on AB
A is center
B is center + axis

PI = PA + AI
AI = dot(AP,AB)/dot(AB,AB)xAB

for more info : 
https://gamedev.stackexchange.com/questions/72528/how-can-i-project-a-3d-point-onto-a-3d-line

Dot(AB,AB) is the lenght of the vector. Because AB is normalized, we can simplify the formula with

PI = PA + dot(AP,AB)xAB 

Example in python
from numpy import *
def ClosestPointOnLine(a, b, p):
    ap = p-a
    ab = b-a
    result = a + dot(ap,ab)/dot(ab,ab) * ab
    return result
*/
#ifndef _VFX_COMMON_H_
#define _VFX_COMMON_H_

float3 ClosestPointOnALine(float3 pos, float3 axis, float3 center)
{
    float3 result;

    float3 PA = center - pos;
    float3 AP = pos - center;
    
    result = PA + (dot(AP,axis)/dot(axis, axis)) * axis;

    return result;
}


#endif // _VFX_COMMON_H_
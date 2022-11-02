//UNITY_SHADER_NO_UPGRADE
#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED
float2 rd(float t) { return frac(sin(float2(dot(floor(t), 45.23), dot(floor(t), 98.236)))*7845.236) - 0.5; }

float2 no(float t) { return lerp(rd(t), rd(t + 1.), smoothstep(0., 1., frac(t))); }

float2 rd(float2 uv) { return frac(sin(float2(dot(uv,float2( 45.23,78.23)), dot(uv, float2(98.236,124.98))))*7845.236); }
void deformin_float( float2 uv, UnityTexture2D A,float val,float val2,  out float3 Out)
{
	

	float3 c = float3(0., 0., 0.);
	float b = sqrt(200.);
	float v2 = val + val2 * pow(length(uv.y - 0.5)*2., 2.);
	float vv = v2 + rd(uv)*v2;
	for (float i = -0.5*b; i < 0.5*b; i += 1.)
		for (float j = -0.5*b; j < 0.5*b; j += 1.) {
			c += tex2D(A, uv + float2(i, j)*0.01*vv ).xyz;
		}
	c /= 200.;

	Out = c;

}
//void deformin2_float (float2 uv, float)
#endif //MYHLSLINCLUDE_INCLUDED

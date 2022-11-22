//UNITY_SHADER_NO_UPGRADE
#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED
float gaussian(float2 i, int samples, float sigma) {
	return exp(-.5* dot(i /= sigma, i)) / (6.28 * sigma*sigma);
}
float hs(float2 uv) {return frac(sin(dot(uv, float2(45.23, 98.56)))*7845.236);}
void blur_float(float2 uv,float  Size , UnityTexture2D A, out float3 Out)
{
	
	float Pi = 6.28318530718;

	float Directions = 16.0; 
	float Quality = 4.0; 
	
	

	float2 Radius = Size /float2(1920.,1080.);
	float4 Color = tex2D(A, uv);
	for (float d = 0.0; d < Pi; d += Pi / Directions)
	{
		for (float i = 1.0 / Quality; i <= 1.0; i += 1.0 / Quality)
		{
			Color += tex2D(A, uv + float2(cos(d), sin(d))*Radius*i);
		}
	}
	Color /= Quality * Directions - 15.;


	Out = Color;

}
void blur2_float(float2 uv, float  Size, UnityTexture2D A, out float3 Out)
{

	float Pi = 6.28318530718;

	float Directions = 16.0;
	float Quality = 4.0;



	float2 Radius = Size / float2(1920., 1080.);
	float di = Radius + Radius * hs(uv);
	float4 Color = tex2D(A, uv);
	for (float d = 0.0; d < Pi; d += Pi / Directions)
	{
		for (float i = 1.0 / Quality; i <= 1.0; i += 1.0 / Quality)
		{
			Color += tex2D(A, uv + float2(cos(d), sin(d))*di*i);
		}
	}
	Color /= Quality * Directions - 15.;


	Out = Color;

}

//void deformin2_float (float2 uv, float)
#endif //MYHLSLINCLUDE_INCLUDED

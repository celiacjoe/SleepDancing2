//UNITY_SHADER_NO_UPGRADE
#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED
float gaussian(float2 i, int samples, float sigma) {
	return exp(-.5* dot(i /= sigma, i)) / (6.28 * sigma*sigma);
}
float hs(float2 uv) {return frac(sin(dot(uv, float2(45.23, 98.56)))*7845.236);}

float no(float2 p) {
	float2 f = floor(p); p = smoothstep(0., 1., frac(p));
	float2 e = float2(45., 98.); float2 v1 = dot(f, e) + float2(0., e.y);
	float2 v2 = lerp(frac(sin(v1)*7845.236), frac(sin(v1 + e.x)*7845.236), p.x);
	return lerp(v2.x, v2.y, p.y);
}
float no(float p) {
	float f = floor(p);
	return lerp(frac(sin(dot(f, 45.23))*785.236), frac(sin(dot(f + 1., 45.23))*785.236), smoothstep(0., 1., frac(p)));
}

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
void liquide_float(float2 uv, float time,  out float2 Out)
{
	float t1 = (pow(length(frac(uv.x*10.) - 0.5)*2., 0.2) - 0.5);
	float t2 = no(uv*float2(140., 0.5) + float2((no(uv*float2(40., 1.5) + float2(time*0.15, 0.)) - 0.5)*3., time*0.1));
	float t3 = (pow(t2, 5.));
	//float t4 = no(t2*5.);
	float t5 = (no(no(t2*0.08)*40. + time * 0.5 + uv.y*2.*no(uv.x*40. + time * 0.1)) - 0.5)*0.25;
	Out = float2(t3 - 0.5, t5);
}
//void deformin2_float (float2 uv, float)
#endif //MYHLSLINCLUDE_INCLUDED

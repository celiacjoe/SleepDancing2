//UNITY_SHADER_NO_UPGRADE
#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED
float2 rd(float t) { return frac(sin(float2(dot(floor(t), 45.23), dot(floor(t), 98.236)))*7845.236) - 0.5; }

float2 no(float t) { return lerp(rd(t), rd(t + 1.), smoothstep(0., 1., frac(t))); }

void deformin_float( float2 uv, UnityTexture2D A, UnityTexture2D B, float time, out float3 Out)
{
	
	float3 t1 = tex2D(A, uv).xyz;
	float3 c1 = t1;
	float ang = 0.2;
	for (int i = 0; i < 6; i++) {
		float3 t2 = tex2D(A, uv + no(i + time * 0.05)*ang).xyz;
		float3 t3 = tex2D(B, uv + no(i + 123. + time * 0.05)*ang).xyz;
		c1 = lerp(c1, t2, smoothstep(0.5, 0., t3.xyz));
	}
	Out = float3(smoothstep(-0.08, 0.95, c1 + float3(0.1, 0.05, 0.05)));

}
//void deformin2_float (float2 uv, float)
#endif //MYHLSLINCLUDE_INCLUDED

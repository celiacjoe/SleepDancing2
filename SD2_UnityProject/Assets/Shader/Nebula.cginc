//UNITY_SHADER_NO_UPGRADE
#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED
float2x2 rot(float t) { float c = cos(t); float s = sin(t); return float2x2(c, -s, s, c); }
float desaturate(float3 color)
{
	float3 lum = float3(0.299, 0.587, 0.114);
	float v1 = dot(lum, color);

	return v1;
}
void nebula_float(float2 uv, float tr  , UnityTexture2D A, UnityTexture2D B, out float3 Out)
{
	float2 e = float2(0.1, 0.);
	float t1 = tex2D(A, uv+e.xy).x;
	float t2 = tex2D(A, uv-e.xy).x;
	float t3 = tex2D(A, uv+e.yx).x;
	float t4 = tex2D(A, uv-e.yx).x;
	float3 t5 = normalize(float3(t1 - t2, t3 - t4, 0.1));
	float2 u2 = (uv-0.5)*2.;
	u2.x *= 16. / 9.;
	u2 = mul(u2, rot(tr*0.1));
	u2.x *= 9. / 16.;
	u2 *= 1. - tr*0.2;
	u2 = u2 * 0.5 + 0.5;
	float3 tf = max(max(tex2D(B, u2 - t5.xy*tr*0.1).xyz, tex2D(B, u2 - t5.xy*tr*0.5).xyz*0.5), tex2D(B, u2 - t5.xy*tr).xyz*0.5);
	float3 tfr = lerp(tf, tex2D(A, uv).xyz, tr);
	float3 col2 = max(tf*(1. - tr), pow(tr, 0.1)*tex2D(A, uv).xyz*max((1. - desaturate(tf)), pow(tr, 10.)));
	Out = tfr;

}
//void deformin2_float (float2 uv, float)
#endif //MYHLSLINCLUDE_INCLUDED

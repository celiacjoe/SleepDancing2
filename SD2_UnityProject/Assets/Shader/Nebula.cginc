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
float3 nor(float2 uv, UnityTexture2D A, UnityTexture2D B) {
	float2 e = float2(0.1, 0.);
	float t1 = tex2D(A, uv + e.xy).x;
	float t2 = tex2D(A, uv - e.xy).x;
	float t3 = tex2D(A, uv + e.yx).x;
	float t4 = tex2D(A, uv - e.yx).x;
	return  normalize(float3(t1 - t2, t3 - t4, 0.1));
}
float3 transi(float2 uv, float tr, UnityTexture2D A, UnityTexture2D B) {
	float3 t5 = nor(uv, A, B);
	float2 u2 = (uv - 0.5)*2.;
	u2.x *= 16. / 9.;
	u2 = mul(u2, rot(tr*0.1));
	u2.x *= 9. / 16.;
	u2 *= 1. - tr * 0.2;
	u2 = u2 * 0.5 + 0.5;
	float3 tf = max(max(tex2D(B, u2 - t5.xy*tr*0.1).xyz, tex2D(B, u2 - t5.xy*tr*0.5).xyz*0.5), tex2D(B, u2 - t5.xy*tr).xyz*0.5);
	return lerp(tf, tex2D(A, uv).xyz,tr);
}
void nebula_float(float2 uv, float tr  , UnityTexture2D A, UnityTexture2D B, UnityTexture2D C, UnityTexture2D D,
	int a, int b, int c , int d,int e, int f,int g, int h, int i,  out float3 Out)
{
	float3 v1 = float3(0., 0., 0.);

	if (a > 0) { v1 = transi(uv, tr, B, A); }
	if (b > 0) { v1 = transi(uv, tr, C, A); }
	if (c > 0) { v1 = transi(uv, tr, A, B); }
	if (d > 0) { v1 = transi(uv, tr, C, B); }
	if (e > 0) { v1 = transi(uv, tr, A, C); }
	if (f > 0) { v1 = transi(uv, tr, B, C); }

	Out = v1;

}
//void deformin2_float (float2 uv, float)
#endif //MYHLSLINCLUDE_INCLUDED

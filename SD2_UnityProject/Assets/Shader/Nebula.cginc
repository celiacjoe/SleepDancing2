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
float hs(float2 uv) { return frac(sin(dot(uv, float2(45.23, 98.56)))*7845.236); }
float3 nor(float2 uv, UnityTexture2D A) {
	float2 e = float2(0.1, 0.);
	float t1 = tex2D(A, uv + e.xy).x;
	float t2 = tex2D(A, uv - e.xy).x;
	float t3 = tex2D(A, uv + e.yx).x;
	float t4 = tex2D(A, uv - e.yx).x;
	return  normalize(float3(t1 - t2, t3 - t4, 0.1));
}
float3 transi(float2 uv, float tr, UnityTexture2D A, UnityTexture2D B) {
	float3 t5 = nor(uv, A);
	float2 u2 = (uv - 0.5)*2.;
	u2.x *= 16. / 9.;
	u2 = mul(u2, rot(tr*0.1));
	u2.x *= 9. / 16.;
	u2 *= 1. - tr * 0.2;
	u2 = u2 * 0.5 + 0.5;
	float3 tf = max(max(tex2D(B, u2 - t5.xy*tr*0.1).xyz, tex2D(B, u2 - t5.xy*tr*0.5).xyz*0.5), tex2D(B, u2 - t5.xy*tr).xyz*0.5);
	return lerp(tf, tex2D(A, uv).xyz,tr);
}
float3 blur2(float2 uv, float  Size, UnityTexture2D A)
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
	return Color;
}
float3 transivid(float2 uv, float tr,  float bl, UnityTexture2D A,  UnityTexture2D B) {
	float3 t5 = nor(uv, A);
	float2 u2 = (uv - 0.5)*2.;
	u2.x *= 16. / 9.;
	u2 = mul(u2, rot(tr*0.1));
	u2.x *= 9. / 16.;
	u2 *= 1. - tr * 0.2;
	u2 = u2 * 0.5 + 0.5;

	float3 tf = blur2(u2+t5*tr,bl,B);
	return lerp(tf, tex2D(A, uv).xyz, tr);
}
float3 transiFX(float2 uv, float tr, float bl, UnityTexture2D A, UnityTexture2D B) {
	float3 blu = blur2(uv, bl, A);
	float2 u2 = (uv - 0.5)*2.;
	u2.x *= 16. / 9.;
	u2 = mul(u2, rot(tr*0.1));
	u2.x *= 9. / 16.;
	u2 *= 1. - tr * 0.2;
	u2 = u2 * 0.5 + 0.5;
	float3 tf = max(max(tex2D(B, u2 - blu.xy*tr*0.1).xyz, tex2D(B, u2 - blu.xy*tr*0.5).xyz*0.5), tex2D(B, u2 - blu.xy*tr).xyz*0.5);
	return lerp(tf, blu, tr);
}
void nebula_float(float2 uv, float tr , UnityTexture2D A, UnityTexture2D B, UnityTexture2D C, UnityTexture2D D,
	int a, int b, int c , int d,int e, int f,int g, int h, int i,int j ,int k, int l, float bl,  out float3 Out)
{
	float3 v1 = float3(0., 0., 0.);

	if (a > 0) { v1 = transi(uv, tr, B, A); }
	if (b > 0) { v1 = transiFX(uv, tr, bl, C, A); }
	if (c > 0) { v1 = transi(uv, tr, D, A); }
	if (d > 0) { v1 = transi(uv, tr, A, B); }
	if (e > 0) { v1 = transiFX(uv, tr, bl, C, B); }
	if (f > 0) { v1 = transi(uv, tr, D, B); }
	if (g > 0) { v1 = transivid(uv, tr, bl, A, C); }
	if (h > 0) { v1 = transivid(uv, tr, bl, B, C); }
	if (i > 0) { v1 = transivid(uv, tr, bl, D, C); }
	if (j > 0) { v1 = transi(uv, tr, B, D); }
	if (k > 0) { v1 = transi(uv, tr, A, D); }
	if (l > 0) { v1 = transiFX(uv, tr, bl, C, D); }
	Out = v1;

}

float no(float2 p, float scale) {
	p *= scale;
	float2 f = floor(p); p = frac(p);
	float2 se = float2(284., 26.); float2 v1 = dot(se, f) + float2(0., se.y);
	float ta = 784526;
	float2 v2 = lerp(frac(sin(v1)*ta), frac(sin(v1 + se.x)*ta), p.x);
	return lerp(v2.x, v2.y, p.y);
}


float it2(float2 p) {
	float r = 0.0; float scale = 5.;
	float a = 0.5; for (int i = 0; i < 2; i++) {
		r += no(p, scale) * a; a *= .5; scale *= 2.;
	}
	return r;
}
float fmb(float2 uv, float time) {
	float2 q = float2(it2(uv) + sin(time*0.2), it2(uv + float2(8.4, 4.3)) + sin(time*0.01));
	return pow(it2(uv + q *2.), 10.);

}
void normal_float(float2 uv, float  time, int bo ,float4 tex, out float4 Out)
{
	if (bo > 0.5) {
		float2 e = float2(0.0001, 0.);
		uv.x *= 1.8;
		uv *= 3.;
		float t1 = fmb(uv, time);
		float t2 = fmb(uv + e.xy, time);
		float t3 = fmb(uv + e.yx, time);
		float3 n1 = normalize(float3(t1 - t2, t1 - t3, 0.01));
		float t4 = pow(t1, 0.5);

		Out = float4(n1, t4);
	}
	else {
		Out = tex;
	}
}

//void deformin2_float (float2 uv, float)
#endif //MYHLSLINCLUDE_INCLUDED

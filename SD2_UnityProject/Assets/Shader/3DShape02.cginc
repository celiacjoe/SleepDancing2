//UNITY_SHADER_NO_UPGRADE
#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED
float3 smin(float3 d1, float3 d2, float k) {
	float3 h = clamp(0.5 + 0.5*(d2 - d1) / k, 0.0, 1.0);
	return lerp(d2, d1, h) - k * h*(1.0 - h);
}

float3 mod(float3 x, float3 y)
{
	return x - y * floor(x / y);
}

float2x2 rot(float t) { float c = cos(t); float s = sin(t); return float2x2(c, -s, s, c); }



float3 rer(float3 p, float r) {
	float at = atan2(p.z, p.x); float t = 6.28 / r;
	float a = mod(at, t) - 0.5*t;
	float2 v = float2(cos(a), sin(a))*length(p.xz);
	return float3(v.x, p.y, v.y);
}
float cap(float3 p, float3 a, float3 b) {
	float3 pa = p - a; float3 ba = b - a;
	float h = clamp(dot(pa, ba) / dot(ba, ba), 0., 1.);
	return length(pa - ba * h);
}

float no(float3 p) {
	float3 f = floor(p); p = smoothstep(0., 1., frac(p));
	float3 se = float3(5., 48., 958.);
	float4 v1 = dot(f, se) + float4(0., se.y, se.z, se.y + se.z);
	float4 v2 = lerp(frac(sin(v1)*7845.236), frac(sin(v1 + se.x)*7845.236), p.x);
	float2 v3 = lerp(v2.xz, v2.yw, p.y);
	return lerp(v3.x, v3.y, p.z);
}

float map(float3 p, float sm, int co, float ta, float3 f1, float3 f2, UnityTexture3D A,float mv ) {
	p.xz = mul(p.xz,rot(p.y*0.5*sign(p.y)));
	float tt =no(p*1.2+mv);
	p.y += sin(p.x + p.z );
	p.y = abs(p.y);
	p = rer(p, 4.);
	float3 p2 = p + float3(tt*-1. - 3., 0., 0.);
	//p2.x = clamp(p2.x, -2., 2.);
	return cap(p2, float3(0., 2., 0.), float3(0., 10., 0.)) - 1.;
}

float3 nor(float3 p, float sm, int co, float ta, float3 f1, float3 f2, UnityTexture3D A, float mv)
{
	float2 e = float2(0.001, 0.); return normalize(map(p, sm, co, ta, f1, f2,A,mv) 
		- float3(map(p - e.xyy, sm, co, ta, f1, f2, A, mv), map(p - e.yxy, sm, co, ta, f1, f2, A, mv), map(p - e.yyx, sm, co, ta, f1, f2, A, mv)));
}
void Shape_float(float2 uv,float di, float fo,float mp,float mvx,float mvy,float mvz ,float sm,
	float co,float ta,float3 f1 , float3 f2,float detail , UnityTexture3D A, out float4 Out)
{
	
	uv = (uv - 0.5)*2.;
	uv.x *=16. / 9.;
	float3 r = normalize(float3(uv, fo));
	float3 p = float3(0., 0., -mp);
	p.yz = mul(p.yz, rot(mvx));
	r.yz = mul(r.yz, rot(mvx));
	p.xz = mul(p.xz, rot(mvy));
	r.xz = mul(r.xz, rot(mvy));
	/*p.xy = mul(p.xy, rot(mvz));
	r.xy = mul(r.xy, rot(mvz));*/
	float dd = 0.;
	int co2 = int(clamp(co*10., 1., 10.));
	/*[unroll(30)]*/for (int i = 0; i < int(detail); i++) {
		float d = map(p,sm,co2, ta,f1,f2, A, mvz);
		if (dd > di) { dd = di; break; }
		if (d < -1.) { break; }
		p += r * d;
		dd += d;
	}
	float d = smoothstep(di, 0., dd);
	float3 n = clamp(nor(p, sm, co2, ta, f1, f2, A, mvz),-1.,1.);


	Out = float4(n,d);

}

//void deformin2_float (float2 uv, float)
#endif //MYHLSLINCLUDE_INCLUDED

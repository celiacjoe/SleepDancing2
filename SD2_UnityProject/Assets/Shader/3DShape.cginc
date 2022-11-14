//UNITY_SHADER_NO_UPGRADE
#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED
float3 smin(float3 d1, float3 d2, float k) {
	float3 h = clamp(0.5 + 0.5*(d2 - d1) / k, 0.0, 1.0);
	return lerp(d2, d1, h) - k * h*(1.0 - h);
}
float2x2 rot(float t) { float c = cos(t); float s = sin(t); return float2x2(c, -s, s, c); }
float map(float3 p,float sm,int co,float ta, float3 f1, float3 f2) {
	float3 b = p;
	for (int i = 0; i < co; i++) {
		float3 bbb = b / dot(b, b);
		float3 bb = smin(bbb, -bbb, -0.2*sm);
		b = f1*bb;
		b -= f2;
	}


	float v2 = length(b) -ta;

	return v2;
}
float3 nor(float3 p, float sm, int co,float ta,float3 f1,float3 f2) 
{ float2 e = float2(0.01, 0.); return normalize(map(p, sm, co,ta, f1, f2) - float3(map(p - e.xyy, sm, co, ta, f1, f2), map(p - e.yxy, sm, co, ta, f1, f2), map(p - e.yyx, sm, co, ta, f1, f2))); }

void Shape_float(float2 uv,float di, float fo,float mp,float mvx,float mvy,float mvz ,float sm, float co,float ta,float3 f1 , float3 f2, out float4 Out)
{
	
	uv = (uv - 0.5)*2.;
	uv.x *=16. / 9.;
	float3 r = normalize(float3(uv, fo));
	float3 p = float3(0., 0., -mp);
	p.yz = mul(p.yz, rot(mvx));
	r.yz = mul(r.yz, rot(mvx));
	p.xz = mul(p.xz, rot(mvy));
	r.xz = mul(r.xz, rot(mvy));
	p.xy = mul(p.xy, rot(mvz));
	r.xy = mul(r.xy, rot(mvz));
	float dd = 0.;
	int co2 = int(clamp(co*10., 1., 10.));
	for (int i = 0; i < 64; i++) {
		float d = map(p,sm,co2, ta,f1,f2);
		if (dd > 30.) { dd = di; break; }
		if (d < -1.) { break; }
		p += r * d;
		dd += d;
	}
	float d = smoothstep(di, 0., dd);
	float3 n = clamp(nor(p, sm, co2, ta, f1, f2),-1.,1.);


	Out = float4(n,d);

}
//void deformin2_float (float2 uv, float)
#endif //MYHLSLINCLUDE_INCLUDED

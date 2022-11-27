//UNITY_SHADER_NO_UPGRADE
#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED
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
	float a = 0.5; for (int i = 0; i <2; i++) {
		r += no(p, scale) * a; a *= .5; scale *= 2.;
	}
	return r;
}
float fmb(float2 uv,float time) {
	float2 q = float2(it2(uv) + sin(time*0.015), it2(uv + float2(8.4, 4.3)) + sin(time*0.01));
	return pow(it2(uv + q * 5.), 10. - 5.);

}
void normal_float(float2 uv,float  time ,  out float3 Out)
{
	
	float2 e = float2(0.0001, 0.);
	
	float t1 = fmb(uv ,time);
	float t2 = fmb(uv  + e.xy, time);
	float t3 = fmb(uv  + e.yx, time);
	float3 n1 = normalize(float3(t1 - t2, t1 - t3, 0.1));
	float t4 = pow(t1, 0.8)*frac(sin(dot(uv, float2(45.6, 98.2)))*7845.236);

	Out = float3(n1.xy,t4);

}

//void deformin2_float (float2 uv, float)
#endif //MYHLSLINCLUDE_INCLUDED

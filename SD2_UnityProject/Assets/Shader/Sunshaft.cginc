//UNITY_SHADER_NO_UPGRADE
#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED
float4 rd(float p) {
	p = floor(p);
	return frac(sin(float4(dot(p, 45.), dot(p, 98.), dot(p, 632.),
		dot(p, 144.)))*7845.236);
}
float4 no(float p) { return lerp(rd(p), rd(p + 1.), frac(p)); }
float rd1(float p) {
	p = floor(p);
	return frac(sin(dot(p, 45.))*7845.236);
}
float no1(float p) {
	return lerp(rd1(p), rd1(p + 1.), frac(p));
}
float3 lensflares(float2 uv, float2 pos,float2 p2,float time)
{
	float2 main = uv - pos;
	float2 uvd = uv * (length(uv));

	float ang = atan2(main.y, main.x);
	float dist = length(main);
	dist = pow(dist, 0.1);

	float r1 = 0.;
	float r2 = 0.;
	float r3 = 0.;
	float2 uvx = uv;
	for (int i = 0; i < 8; i++) {
		float4 ra = no(float(i + 5)*26. +time*0.1);
		float4 ra2 = ra * float4(0.6, 0.8, 7., 2.);
		float4 ra3 = ra * float4(20., 2.5, 0.5, 2.);
		float4 ra4 = no(float(i + 24)*26.+time*0.1 + 0.5);

		uvx = lerp(lerp(uv*lerp(1., ra4.x, smoothstep(0.4, 0.6, ra4.a)),
			uvx*pow(ra4.y, 2.), ra4.z), uvd, -ra2.a);
		r1 += max(0.01 - pow(length(uvx + ra2.x*pos), pow(ra2.z, 0.8)), .0)*6.0;
		r2 += max(0.01 - pow(length(uvx + (ra2.x + ra2.y)*pos), pow(ra2.z, 0.8)), .0)*5.0;
		r3 += max(0.01 - pow(length(uvx + (ra2.x + ra2.y*2.)*pos), pow(ra2.z, 0.8)), .0)*3.0;
		r1 += max(1.0 / (1.0 + 32.0*pow(length(uvx + ra3.x*pos), ra3.y)), .0)*0.25;
		r2 += max(1.0 / (1.0 + 32.0*pow(length(uvx + (ra3.x + ra3.z)*pos), ra3.y)), .0)*0.23;
		r3 += max(1.0 / (1.0 + 32.0*pow(length(uvx + (ra3.x + ra3.z*2.)*pos), ra3.y)), .0)*0.21;
	}

	return float3(r1, r2, r3);
}


void sun_float(float2 uv, float2 p1 , float2 p2 , float time,  out float3 Out)
{
	
	uv = (uv - 0.5)*2.;
	uv.x *= 16. / 9.;


	Out = smoothstep(-0.08, 0.9, pow(lensflares(uv*1.5, float2((p1.x-0.5)*-2.,no1(time*0.2)),p2,time), float3(0.5,0.5,0.5)));

}
//void deformin2_float (float2 uv, float)
#endif //MYHLSLINCLUDE_INCLUDED

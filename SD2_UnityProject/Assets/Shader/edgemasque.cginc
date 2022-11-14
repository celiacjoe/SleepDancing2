//UNITY_SHADER_NO_UPGRADE
#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED


void edgemasque_float(float2 uv, UnityTexture2D A, float ex,float pui , out float Out)
{
	float2  e = float2(ex*0.01, 0.);
	float2 t1 = tex2D(A, uv+e.xy).xy;
	float2 t2 = tex2D(A, uv-e.xy).xy;
	float2 t3 = tex2D(A, uv+e.yx*(16/9.)).xy;
	float2 t4 = tex2D(A, uv-e.yx*(16 / 9.)).xy;
	float2 tf = normalize(float3(max(abs(t1 - t2), abs(t3 - t4)),pui)).xy;
	Out = 1.-clamp(max(tf.x,tf.y),0.,1.);

}
//void deformin2_float (float2 uv, float)
#endif //MYHLSLINCLUDE_INCLUDED

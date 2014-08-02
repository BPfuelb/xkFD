sampler ColorMapSampler : register(s0);
// sampler firstSampler;

float4 PS_COLOR(float2 texCoord: TEXCOORD0) : COLOR
{


   float4 color = tex2D(ColorMapSampler, texCoord);
	color.rgb = 1.0f - color.rgb; 
 

	return color;
} 

technique invertiereFarbe
{
   pass pass0
   {
      PixelShader = compile ps_2_0 PS_COLOR();
   }
} 
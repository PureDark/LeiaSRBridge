// Leia's optional shader conversions. Optical selection/interpolation is unchanged.
cbuffer PixelOrigin : register(b1) { float2 ViewportOrigin; float ReadSrgb; float WriteSrgb; };
float3 DecodeSrgb(float3 v) { return v <= 0.04045 ? v / 12.92 : pow(max((v + 0.055) / 1.055, 0.0), 2.4); }
float3 EncodeSrgb(float3 v) { return v <= 0.0031308 ? v * 12.92 : 1.055 * pow(max(v, 0.0), 1.0 / 2.4) - 0.055; }
float4 ReadInput(int3 location) {
    float4 value = InputTexture.Load(location);
    if (ReadSrgb != 0) value.rgb = DecodeSrgb(value.rgb);
    return value;
}
float4 ConvertOutput(float4 value) {
    if (WriteSrgb != 0) value.rgb = EncodeSrgb(value.rgb);
    return value;
}

Texture2D<float4> InputTexture : register(t0);
cbuffer OpticalConstants : register(b0) { uint4 Constants[5]; };
cbuffer PixelOrigin : register(b1) { float2 ViewportOrigin; uint2 OriginPadding; };
float4 main(float4 position : SV_Position) : SV_Target {
    uint2 pixel = uint2(max(position.xy - ViewportOrigin, 0));
    uint2 location = pixel * Constants[4].xy / max(Constants[3].zw, 1);
    location.x >>= 1;
    return InputTexture.Load(int3(location, 0));
}

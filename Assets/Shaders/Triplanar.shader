Shader "Custom/TriplanarModelSpace"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0
        sampler2D _MainTex;
        float4 _MainTex_ST;

        struct Input
        {
            float3 worldNormal;
            float3 worldPos;
        };

        float3 TriplanarUVs(float3 worldPos, float4 scaleOffset)
        {
            float3 blendedUVs;
            blendedUVs.xz = worldPos.xz * scaleOffset.xy + scaleOffset.zw;
            blendedUVs.xy = worldPos.xy * scaleOffset.xy + scaleOffset.zw;
            blendedUVs.yz = worldPos.yz * scaleOffset.xy + scaleOffset.zw;
            return blendedUVs;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 worldNormal = normalize(IN.worldNormal);
            float3 absNormal = abs(worldNormal);

            float3 blendedUVs = TriplanarUVs(IN.worldPos, _MainTex_ST);

            float3 blendWeights = absNormal / dot(absNormal, float3(1,1,1));

            half4 texX = tex2D(_MainTex, blendedUVs.yz);
            half4 texY = tex2D(_MainTex, blendedUVs.xz);
            half4 texZ = tex2D(_MainTex, blendedUVs.xy);

            o.Albedo = texX * blendWeights.x + texY * blendWeights.y + texZ * blendWeights.z;
            o.Alpha = 1.0;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
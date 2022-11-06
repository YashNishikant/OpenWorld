Shader "Custom/Terrain"
{
    Properties{
        _AngleFloat ("steep1", Float) = 0
        _AngleFloat2 ("steep2", Float) = 0
        _AngleFloat3 ("steep3", Float) = 0
        _AngleFloat4 ("steep4", Float) = 0
    }
        SubShader{
          CGPROGRAM
          #pragma surface surf Lambert vertex:vert

          struct Input {
              float3 customColor;
              float3 worldNormal;
              float3 worldPos;
          };

        void vert(inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o)

        }
        float _AngleFloat;
        float _AngleFloat2;
        float _AngleFloat3;
        float _AngleFloat4;

        void surf(Input IN, inout SurfaceOutput o) {

                
              float3 worldNormal = WorldNormalVector(IN, o.Normal);
              half w = dot(worldNormal, normalize(half3(0, 1, 0)));
              half h = IN.worldPos.y;

              if (abs(w) < _AngleFloat4) { // flattest
                  IN.customColor = float3(0, 0.57, 0.4);
              }
              if (abs(w) < _AngleFloat3) { // second flattest
                    IN.customColor = float3(0, 0.6, 0.35);
              }
              if (abs(w) < _AngleFloat2) { // second steepest
                  IN.customColor = float3(0.53, 0.41, 0);
              }
//              if (abs(w) < _AngleFloat) { // steepest
//                  IN.customColor = float3(0.35, 0.27, 0);
//              }

              if (h < 10) {
                  IN.customColor = float3(0.99, 0.86, 0.57);
              }

              if (h > 88 && abs(w) > 0.5) {
                  IN.customColor = float3(1, 1, 1);
              }

              o.Albedo = IN.customColor;
        }
        ENDCG
    }
        Fallback "Diffuse"
}
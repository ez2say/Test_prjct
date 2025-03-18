Shader "Custom/OutlineShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0, 1, 0, 1)
        _OutlineWidth ("Outline Width", Range(0.0, 0.1)) = 0.02
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        // Pass для рисования Outline
        Pass
        {
            Cull Front // Рисуем только обратные грани
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float _OutlineWidth;
            fixed4 _OutlineColor;

            v2f vert(appdata v)
            {
                v2f o;

                // Преобразуем вершину в пространство клипа
                o.pos = UnityObjectToClipPos(v.vertex);

                // Вычисляем направление нормали в пространстве камеры
                float3 norm = normalize(mul((float3x3)UNITY_MATRIX_MV, v.normal));

                // Применяем смещение только к граням, которые направлены в сторону камеры
                float2 offset = norm.xy * _OutlineWidth;

                // Учитываем угол между нормалью и направлением камеры
                float dotProduct = dot(norm, float3(0, 0, 1)); // Направление камеры (Z-ось)
                if (dotProduct > 0) // Только если нормаль направлена в сторону камеры
                {
                    o.pos.xy += offset * o.pos.w;
                }

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }

        // Pass для рисования основного объекта
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
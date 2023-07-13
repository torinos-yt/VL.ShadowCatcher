using System;
using System.Collections.Generic;
using System.Reflection;
using Stride.Core.Mathematics;
using Stride.Rendering.Lights;
using Stride.Rendering.Shadows;
using static Stride.Rendering.Shadows.LightDirectionalShadowMapRenderer;

namespace VL.ShadowCatcher
{

public static class Reflection
{
    static LightSpotShadowMapShaderDataFields _spotShaderDataFields;
    static LightPointCubeShadowMapShaderDataFileds _pointShaderDataFields;

    static Reflection()
    {
        var assembly = Assembly.GetAssembly(typeof(LightSpotShadowMapRenderer));
        var spotShaderDataType = assembly.GetType("Stride.Rendering.Shadows.LightSpotShadowMapRenderer+LightSpotShadowMapShaderData");
        var pointShaderDataType = assembly.GetType("Stride.Rendering.Shadows.LightPointShadowMapRendererCubeMap+ShaderData");

        var spotDataFields = spotShaderDataType.GetFields();
        var pointDataFields = pointShaderDataType.GetFields();
        _spotShaderDataFields = new LightSpotShadowMapShaderDataFields
        {
            Texture =                spotDataFields[0],
            DepthBias =              spotDataFields[1],
            OffsetScale =            spotDataFields[2],
            DepthRange =             spotDataFields[3],
            WorldToShadowCascadeUV = spotDataFields[4],
            ViewMatrix =             spotDataFields[5],
            ProjectionMatrix =       spotDataFields[6],
        };

        _pointShaderDataFields = new LightPointCubeShadowMapShaderDataFileds
        {
            Texture =         pointDataFields[0],
            FaceOffsets =     pointDataFields[1],
            DepthParameters = pointDataFields[2],
            Projection =      pointDataFields[3],
            View =            pointDataFields[4],
            WorldToShadow =   pointDataFields[5],
            DepthBias =       pointDataFields[6],
            OffsetScale =     pointDataFields[7],
        };
    }

    public static List<LightShadowMapTexture> GetShadowMaps(ShadowMapRenderer feature)
    {
        if(feature == null) return null;

        Type info = feature.GetType();
        var field = info.GetField("shadowMaps", BindingFlags.Instance | BindingFlags.NonPublic);

        return (List<LightShadowMapTexture>)field.GetValue(feature);
    }

    public static void GetLightShadowShaderDatas(
        List<LightShadowMapTexture> shadows,
        out List<ShaderData> directional,
        out List<ReflectionLightSpotShadowMapShaderData> spot,
        out List<ReflectionLightPointCubeShadowMapShaderData> point)
    {
        var d = new List<ShaderData>();
        var s = new List<ReflectionLightSpotShadowMapShaderData>();
        var p = new List<ReflectionLightPointCubeShadowMapShaderData>();

        foreach(var shadow in shadows)
        {
            var data = shadow.ShaderData;
            var type = shadow.RenderLight.Type;

            if(type is LightDirectional)
                d.Add(data as ShaderData);
            else if(type is LightSpot)
                s.Add(_spotShaderDataFields.GetFields(data));
            else if(type is LightPoint)
                p.Add(_pointShaderDataFields.GetFields(data));
        }

        directional = d;
        spot        = s;
        point       = p;
    }

    public static Vector3 GetSpotLightParam(LightSpot spot)
    {
        Type type = spot.GetType();
        var LightAngleScale = type.GetField("LightAngleScale", BindingFlags.Instance | BindingFlags.NonPublic);
        var LightAngleOffset = type.GetField("LightAngleOffset", BindingFlags.Instance | BindingFlags.NonPublic);
        var InvSquareRange = type.GetField("InvSquareRange", BindingFlags.Instance | BindingFlags.NonPublic);

        return new Vector3
        {
            X = (float)LightAngleScale.GetValue(spot),
            Y = (float)LightAngleOffset.GetValue(spot),
            Z = (float)InvSquareRange.GetValue(spot),
        };
    }
}

}

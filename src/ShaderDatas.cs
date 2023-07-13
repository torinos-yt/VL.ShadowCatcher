using System;
using System.Reflection;
using Stride.Core.Mathematics;
using Stride.Graphics;
using Stride.Rendering.Shadows;

namespace VL.ShadowCatcher
{
    public struct ReflectionLightSpotShadowMapShaderData : ILightShadowMapShaderData
    {
        public Texture Texture;
        public float DepthBias;
        public float OffsetScale;
        public Vector2 DepthRange;
        public Matrix WorldToShadowCascadeUV;
        public Matrix ViewMatrix;
        public Matrix ProjectionMatrix;
    }

    public struct LightSpotShadowMapShaderDataFields : ILightShadowMapShaderData
    {
        public FieldInfo Texture;
        public FieldInfo DepthBias;
        public FieldInfo OffsetScale;
        public FieldInfo DepthRange;
        public FieldInfo WorldToShadowCascadeUV;
        public FieldInfo ViewMatrix;
        public FieldInfo ProjectionMatrix;

        public ReflectionLightSpotShadowMapShaderData GetFields(ILightShadowMapShaderData data)
        {
            return new ReflectionLightSpotShadowMapShaderData
            {
                Texture = (Texture)this.Texture.GetValue(data),
                DepthBias = (float)this.DepthBias.GetValue(data),
                OffsetScale = (float)this.OffsetScale.GetValue(data),
                DepthRange = (Vector2)this.DepthRange.GetValue(data),
                WorldToShadowCascadeUV = (Matrix)this.WorldToShadowCascadeUV.GetValue(data),
                ViewMatrix = (Matrix)this.ViewMatrix.GetValue(data),
                ProjectionMatrix = (Matrix)this.ProjectionMatrix.GetValue(data),
            };
        }
    }

    public struct ReflectionLightPointCubeShadowMapShaderData : ILightShadowMapShaderData
    {
        public Texture Texture;
        public Vector2[] FaceOffsets;
        public Vector2 DepthParameters;
        public Matrix Projection;
        public Matrix[] View;
        public Matrix[] WorldToShadow;
        public float DepthBias;
        public float OffsetScale;
    }

    public struct LightPointCubeShadowMapShaderDataFileds
    {
        public FieldInfo Texture;
        public FieldInfo FaceOffsets;
        public FieldInfo DepthParameters;
        public FieldInfo Projection;
        public FieldInfo View;
        public FieldInfo WorldToShadow;
        public FieldInfo DepthBias;
        public FieldInfo OffsetScale;

        public ReflectionLightPointCubeShadowMapShaderData GetFields(ILightShadowMapShaderData data)
        {
            return new ReflectionLightPointCubeShadowMapShaderData
            {
                Texture = (Texture)this.Texture.GetValue(data),
                FaceOffsets = (Vector2[])this.FaceOffsets.GetValue(data),
                DepthParameters = (Vector2)this.DepthParameters.GetValue(data),
                Projection = (Matrix)this.Projection.GetValue(data),
                View = (Matrix[])this.View.GetValue(data),
                WorldToShadow = (Matrix[])this.WorldToShadow.GetValue(data),
                DepthBias = (float)this.DepthBias.GetValue(data),
                OffsetScale = (float)this.OffsetScale.GetValue(data),
            };
        }
    }

    public struct ReflectionLightPointParaboloidShadowMapShaderData : ILightShadowMapShaderData
    {
        public Texture Texture;
        public Vector2 Offset;
        public Vector2 BackfaceOffset;
        public Vector2 FaceSize;
        public Matrix View;
        public Vector2 DepthParameters;
        public float DepthBias;
    }

    public struct LightPointParaboloidShadowMapShaderDataFields : ILightShadowMapShaderData
    {
        public FieldInfo Texture;
        public FieldInfo Offset;
        public FieldInfo BackfaceOffset;
        public FieldInfo FaceSize;
        public FieldInfo View;
        public FieldInfo DepthParameters;
        public FieldInfo DepthBias;

        public ReflectionLightPointParaboloidShadowMapShaderData GetFields(ILightShadowMapShaderData data)
        {
            return new ReflectionLightPointParaboloidShadowMapShaderData
            {
                Texture = (Texture)this.Texture.GetValue(data),
                Offset = (Vector2)this.Offset.GetValue(data),
                BackfaceOffset = (Vector2)this.BackfaceOffset.GetValue(data),
                FaceSize = (Vector2)this.FaceSize.GetValue(data),
                View = (Matrix)this.View.GetValue(data),
                DepthParameters = (Vector2)this.DepthParameters.GetValue(data),
                DepthBias = (float)this.DepthBias.GetValue(data),
            };
        }
    }

}
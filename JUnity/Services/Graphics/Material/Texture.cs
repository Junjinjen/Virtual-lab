﻿using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System.Drawing.Imaging;

namespace JUnity.Services.Graphics.Material
{
    public sealed class Texture
    {
        public Texture(BitmapData imageData, SamplerState samplerState, int mipLevels = -1)
        {
            SamplerState = samplerState;

            var texture2d = new Texture2D(Engine.Instance.GraphicsRenderer.Device, new Texture2DDescription()
            {
                Width = imageData.Width,
                Height = imageData.Height,
                ArraySize = 1,
                BindFlags = mipLevels != -1 ? BindFlags.ShaderResource | BindFlags.RenderTarget : BindFlags.ShaderResource,
                Usage = ResourceUsage.Default,
                CpuAccessFlags = CpuAccessFlags.None,
                Format = Format.B8G8R8A8_UNorm,
                MipLevels = mipLevels != -1 ? 0 : 1,
                OptionFlags = mipLevels != -1 ? ResourceOptionFlags.GenerateMipMaps : ResourceOptionFlags.None,
                SampleDescription = new SampleDescription(1, 0),
            },
            new DataRectangle(imageData.Scan0, imageData.Stride));

            ShaderResourceView = new ShaderResourceView(Engine.Instance.GraphicsRenderer.Device, texture2d, new ShaderResourceViewDescription()
            {
                Dimension = ShaderResourceViewDimension.Texture2D,
                Format = Format.R8G8B8A8_UNorm,
                Texture2D = new ShaderResourceViewDescription.Texture2DResource
                {
                    MostDetailedMip = 0,
                    MipLevels = mipLevels != -1 ? mipLevels : 1
                }
            });

            if (mipLevels != -1)
            {
                var dataBox = new DataBox(imageData.Scan0, imageData.Stride, 1);
                Engine.Instance.GraphicsRenderer.Device.ImmediateContext.UpdateSubresource(dataBox, texture2d, 0);
                Engine.Instance.GraphicsRenderer.Device.ImmediateContext.GenerateMips(ShaderResourceView);
            }
        }

        public ShaderResourceView ShaderResourceView { get; }

        public SamplerState SamplerState { get; }
    }
}

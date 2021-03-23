using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.IO;
using SharpDX.WIC;
using System.IO;

namespace JUnity.Services.Graphics.Meshing
{
    public sealed class Texture
    {
        private static ImagingFactory _imagingFactory = new ImagingFactory();

        public Texture(string filename, int mipLevels = -1)
        {
            using (var decoder = new BitmapDecoder(_imagingFactory, filename, DecodeOptions.CacheOnDemand))
            {
                CreateImage(decoder.GetFrame(0), mipLevels);
            }
        }

        public Texture(byte[] data, int mipLevels = -1)
        {
            using (var stream = new MemoryStream(data))
            {
                using (var decoder = new BitmapDecoder(_imagingFactory, stream, DecodeOptions.CacheOnDemand))
                {
                    CreateImage(decoder.GetFrame(0), mipLevels);
                }
            }
        }

        private void CreateImage(BitmapFrameDecode imageData, int mipLevels = -1)
        {
            var formatConverter = new FormatConverter(_imagingFactory);
            formatConverter.Initialize(imageData, PixelFormat.Format32bppPRGBA,
                BitmapDitherType.None, null, 0.0f, BitmapPaletteType.Custom);

            int stride = formatConverter.Size.Width * 4;
            var buffer = new DataStream(formatConverter.Size.Height * stride, true, true);
            formatConverter.CopyPixels(stride, buffer);

            var textureDesc = new Texture2DDescription
            {
                Width = formatConverter.Size.Width,
                Height = formatConverter.Size.Height,
                ArraySize = 1,
                BindFlags = mipLevels != -1 ? BindFlags.ShaderResource | BindFlags.RenderTarget : BindFlags.ShaderResource,
                Usage = ResourceUsage.Default,
                CpuAccessFlags = CpuAccessFlags.None,
                Format = Format.R8G8B8A8_UNorm,
                MipLevels = mipLevels != -1 ? 0 : 1,
                OptionFlags = mipLevels != -1 ? ResourceOptionFlags.GenerateMipMaps : ResourceOptionFlags.None,
                SampleDescription = new SampleDescription(1, 0),
            };

            Texture2D texture2d;
            if (mipLevels != -1)
            {
                texture2d = new Texture2D(Engine.Instance.GraphicsRenderer.Device, textureDesc);
            }
            else
            {
                var dataRectangle = new DataRectangle(buffer.DataPointer, stride);
                texture2d = new Texture2D(Engine.Instance.GraphicsRenderer.Device, textureDesc, dataRectangle);
            }

            ShaderResourceView = new ShaderResourceView(Engine.Instance.GraphicsRenderer.Device, texture2d, new ShaderResourceViewDescription
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
                var dataBox = new DataBox(buffer.DataPointer, stride, 1);
                Engine.Instance.GraphicsRenderer.Device.ImmediateContext.UpdateSubresource(dataBox, texture2d, 0);
                Engine.Instance.GraphicsRenderer.Device.ImmediateContext.GenerateMips(ShaderResourceView);
            }
        }

        internal ShaderResourceView ShaderResourceView { get; private set; }
    }
}

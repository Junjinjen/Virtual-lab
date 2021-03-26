using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Direct2DBitmap = SharpDX.Direct2D1.Bitmap;
using WicBitmap = SharpDX.WIC.Bitmap;
using SharpDX.WIC;

namespace JUnity.Services.Graphics
{
    public sealed class Texture
    {
        private static ImagingFactory _imagingFactory = new ImagingFactory();
        private ShaderResourceView _shaderResourceView;
        private Direct2DBitmap _direct2DBitmap;

        private readonly WicBitmap _wicBitmap;
        private readonly int _mipLevels;

        public Texture(string filename, int mipLevels = -1)
        {
            _mipLevels = mipLevels;

            using (var decoder = new BitmapDecoder(_imagingFactory, filename, DecodeOptions.CacheOnDemand))
            {
                var formatConverter = new FormatConverter(_imagingFactory);
                formatConverter.Initialize(decoder.GetFrame(0), PixelFormat.Format32bppPRGBA,
                    BitmapDitherType.None, null, 0.0f, BitmapPaletteType.Custom);
                _wicBitmap = new WicBitmap(_imagingFactory, formatConverter, BitmapCreateCacheOption.CacheOnDemand);
            }
        }

        public Texture(Color[] data, int width, int height, int mipLevels = -1)
        {
            _mipLevels = mipLevels;

            _wicBitmap = WicBitmap.New(_imagingFactory, width, height, PixelFormat.Format32bppPRGBA, data);
        }

        internal ShaderResourceView ShaderResourceView
        {
            get
            {
                if (_shaderResourceView == null)
                {
                    CreateShaderResourceView();
                }

                return _shaderResourceView;
            }
        }

        internal Direct2DBitmap Bitmap
        {
            get
            {
                if (_direct2DBitmap == null)
                {
                    CreateBitmap();
                }

                return _direct2DBitmap;
            }
        }

        private void CreateShaderResourceView()
        {
            int stride = _wicBitmap.Size.Width * 4;
            var buffer = new DataStream(_wicBitmap.Size.Height * stride, true, true);
            _wicBitmap.CopyPixels(stride, buffer);

            var textureDesc = new Texture2DDescription
            {
                Width = _wicBitmap.Size.Width,
                Height = _wicBitmap.Size.Height,
                ArraySize = 1,
                BindFlags = _mipLevels != -1 ? BindFlags.ShaderResource | BindFlags.RenderTarget : BindFlags.ShaderResource,
                Usage = ResourceUsage.Default,
                CpuAccessFlags = CpuAccessFlags.None,
                Format = Format.R8G8B8A8_UNorm,
                MipLevels = _mipLevels != -1 ? 0 : 1,
                OptionFlags = _mipLevels != -1 ? ResourceOptionFlags.GenerateMipMaps : ResourceOptionFlags.None,
                SampleDescription = new SampleDescription(1, 0),
            };

            Texture2D texture2d;
            if (_mipLevels != -1)
            {
                texture2d = new Texture2D(Engine.Instance.GraphicsRenderer.Device, textureDesc);
            }
            else
            {
                var dataRectangle = new DataRectangle(buffer.DataPointer, stride);
                texture2d = new Texture2D(Engine.Instance.GraphicsRenderer.Device, textureDesc, dataRectangle);
            }

            _shaderResourceView = new ShaderResourceView(Engine.Instance.GraphicsRenderer.Device, texture2d, new ShaderResourceViewDescription
            {
                Dimension = ShaderResourceViewDimension.Texture2D,
                Format = Format.R8G8B8A8_UNorm,
                Texture2D = new ShaderResourceViewDescription.Texture2DResource
                {
                    MostDetailedMip = 0,
                    MipLevels = _mipLevels != -1 ? _mipLevels : 1
                }
            });

            if (_mipLevels != -1)
            {
                var dataBox = new DataBox(buffer.DataPointer, stride, 1);
                Engine.Instance.GraphicsRenderer.Device.ImmediateContext.UpdateSubresource(dataBox, texture2d, 0);
                Engine.Instance.GraphicsRenderer.Device.ImmediateContext.GenerateMips(ShaderResourceView);
            }
        }

        private void CreateBitmap()
        {
            _direct2DBitmap = Direct2DBitmap.FromWicBitmap(Engine.Instance.UIRenderer.RenderTarget, _wicBitmap);
        }
    }
}

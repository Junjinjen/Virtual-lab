using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using Direct2DFactory = SharpDX.Direct2D1.Factory;
using Direct2DBitmap = SharpDX.Direct2D1.Bitmap;
using Direct2DPixelFormat = SharpDX.Direct2D1.PixelFormat;
using Direct2DAlphaMode = SharpDX.Direct2D1.AlphaMode;
using Direct2DTextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode;
using Direct2DBitmapInterpolationMode = SharpDX.Direct2D1.BitmapInterpolationMode;
using SharpDX.Direct3D11;
using SharpDX.WIC;

namespace JUnity.Services.Graphics
{
    public class UIRenderer : IDisposable
    {
        private RenderTarget _renderTarget;
        private Direct2DFactory _factory2D;
        private RenderTargetProperties _renderTargetProperties;
        private RawRectangleF _renderTargetClientRectangle;
        private ImagingFactory _imagingFactory;

        public UIRenderer()
        {
            _factory2D = new Direct2DFactory();
            _imagingFactory = new ImagingFactory();

            _renderTargetProperties.DpiX = 0;
            _renderTargetProperties.DpiY = 0;
            _renderTargetProperties.MinLevel = FeatureLevel.Level_10;
            _renderTargetProperties.PixelFormat = new Direct2DPixelFormat(
                Format.Unknown,                                           // SharpDX.DXGI.Format.R8G8B8A8_UNorm
                Direct2DAlphaMode.Premultiplied);                         // ????  Straight not supported
            _renderTargetProperties.Type = RenderTargetType.Hardware;     // Default
            _renderTargetProperties.Usage = RenderTargetUsage.None;
        }


        public void OnResize(Texture2D texture)
        {
            _renderTarget?.Dispose();

            Surface surface = texture.QueryInterface<Surface>();
            _renderTarget = new RenderTarget(_factory2D, surface, _renderTargetProperties);
            SharpDX.Utilities.Dispose(ref surface);


            _renderTarget.AntialiasMode = AntialiasMode.PerPrimitive;
            _renderTarget.TextAntialiasMode = Direct2DTextAntialiasMode.Cleartype;
            _renderTargetClientRectangle.Left = 0;
            _renderTargetClientRectangle.Top = 0;
            _renderTargetClientRectangle.Right = _renderTarget.Size.Width;
            _renderTargetClientRectangle.Bottom = _renderTarget.Size.Height;
        }

        public Direct2DBitmap GetBitmap(string fileName)
        {
            BitmapDecoder decoder = new BitmapDecoder(_imagingFactory, fileName, DecodeOptions.CacheOnDemand);

            FormatConverter imageFormatConverter = new FormatConverter(_imagingFactory);
            imageFormatConverter.Initialize(
                decoder.GetFrame(0),
                SharpDX.WIC.PixelFormat.Format32bppPRGBA, // PRGBA = RGB premultiplied to alpha channel!!!!! YoPRST!
                BitmapDitherType.Ordered4x4, null, 0.0, BitmapPaletteType.Custom);

            decoder.Dispose();

            Direct2DBitmap bitmap = Direct2DBitmap.FromWicBitmap(_renderTarget, imageFormatConverter);

            imageFormatConverter.Dispose();

            return bitmap;
        }

        public void DrawTexture(string fileName)
        {
            _renderTarget.BeginDraw();
            _renderTarget.DrawBitmap(GetBitmap(fileName), 1, Direct2DBitmapInterpolationMode.Linear);
            _renderTarget.EndDraw();
        }

        public void Dispose()
        {
            _renderTarget.Dispose();
            _imagingFactory.Dispose();
            _factory2D.Dispose();
        }
    }
}

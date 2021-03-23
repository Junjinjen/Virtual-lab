using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System.Drawing;
using System.Drawing.Imaging;

namespace JUnity.Services.Graphics.Meshing
{
    public sealed class Texture
    {
        public Texture(BitmapData imageData, int mipLevels = -1)
        {
            CreateImage(imageData, mipLevels);
        }

        public Texture(string filename, int mipLevels = -1)
        {
            FileName = filename;
            using (var bmp = new Bitmap(filename))
            {
                var rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
                if (bmp.PixelFormat != PixelFormat.Format32bppArgb)
                {
                    using (var bmpWithFormat = bmp.Clone(rect, PixelFormat.Format32bppArgb))
                    {
                        CreateImage(bmpWithFormat, rect, mipLevels);
                    }
                }
                else
                {
                    CreateImage(bmp, rect, mipLevels);
                }
            }
        }

        private void CreateImage(Bitmap bmp, System.Drawing.Rectangle rect, int mipLevels = -1)
        { 
            var data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            CreateImage(data, mipLevels);
            bmp.UnlockBits(data);
        }

        private void CreateImage(BitmapData imageData, int mipLevels = -1)
        {
            var dataRectangle = new DataRectangle(imageData.Scan0, imageData.Stride);
            var textureDesc = new Texture2DDescription
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
            };

            Texture2D texture2d;
            if (mipLevels != -1)
            {
                texture2d = new Texture2D(Engine.Instance.GraphicsRenderer.Device, textureDesc);
            }
            else
            {
                texture2d = new Texture2D(Engine.Instance.GraphicsRenderer.Device, textureDesc, dataRectangle);
            }

            ShaderResourceView = new ShaderResourceView(Engine.Instance.GraphicsRenderer.Device, texture2d, new ShaderResourceViewDescription
            {
                Dimension = ShaderResourceViewDimension.Texture2D,
                Format = Format.B8G8R8A8_UNorm,
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

        internal ShaderResourceView ShaderResourceView { get; private set; }

        internal string FileName { get; private set; }

    }
}

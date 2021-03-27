﻿using JUnity.Services.Graphics.UI.Surfaces.Interfaces;
using SharpDX;
using SharpDX.Direct2D1;

namespace JUnity.Services.Graphics.UI.Surfaces
{
    public class TexturedRectangle : IRectangleSurface
    {
        public Texture Texture { get; set; }

        public float Opacity { get; set; }

        public void Draw(RenderTarget renderTarget, RectangleF rectangle)
        {
            renderTarget.DrawBitmap(Texture.Bitmap, rectangle,
                    Opacity, BitmapInterpolationMode.Linear);
        }
    }
}
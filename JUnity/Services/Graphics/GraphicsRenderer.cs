using SharpDX.Direct3D11;
using SharpDX.Windows;
using System;
using System.Collections.Generic;

namespace JUnity.Services.Graphics
{
    internal sealed class GraphicsRenderer : IDisposable
    {
        public Dictionary<string, VertexShader> VertexShaders { get; private set; }

        public Dictionary<string, PixelShader> PixelShaders { get; private set; }

        public Device Device { get; private set; }

        public RenderForm RenderForm { get; private set; }

        public void RenderScene()
        {
            throw new NotSupportedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotSupportedException();
        }
    }
}

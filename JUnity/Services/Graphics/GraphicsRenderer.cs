﻿using SharpDX.Direct3D11;
using SharpDX.Windows;
using System;
using System.Collections.Generic;

namespace JUnity.Services.Graphics
{
    internal sealed class GraphicsRenderer : IDisposable
    {
        public Dictionary<string, VertexShader> VertexShaders { get; }

        public Dictionary<string, PixelShader> PixelShaders { get; }

        public Device Device
        {
            get => throw new NotImplementedException();
        }

        public RenderForm RenderForm
        {
            get => throw new NotImplementedException();
        }

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

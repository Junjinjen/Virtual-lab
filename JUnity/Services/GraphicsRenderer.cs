using SharpDX.Windows;
using System;

namespace Engine.Services
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3881:\"IDisposable\" should be implemented correctly",
        Justification = "Will be correctly disposed by JUnity class")]
    internal class GraphicsRenderer : IDisposable
    {
        public RenderForm RenderForm
        {
            get => throw new System.NotImplementedException();
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
            throw new System.NotSupportedException();
        }
    }
}

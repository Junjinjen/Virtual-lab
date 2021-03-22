using System;
using System.IO;
using SharpDX.D3DCompiler;

namespace JUnity.Services.Graphics.Utilities
{
    internal class IncludeHandler : Include
    {
        private string _shaderFolder;
        private Stream _stream;

        public IncludeHandler(string shadersFolder)
        {
            _shaderFolder = shadersFolder;
        }

        public IDisposable Shadow { get; set; }

        public void Close(Stream stream)
        {
            stream?.Dispose();
        }

        public void Dispose()
        {
            _stream?.Dispose();
        }

        public Stream Open(IncludeType type, string fileName, Stream parentStream)
        {
            var path = Path.Combine(_shaderFolder, fileName);
            if (File.Exists(path))
            {
                _stream = new FileStream(Path.Combine(_shaderFolder, fileName), FileMode.Open);
                return _stream;
            }

            _stream = new FileStream(Path.Combine(Path.Combine(_shaderFolder, "Headers"), fileName), FileMode.Open);
            return _stream;
        }
    }
}

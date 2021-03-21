using System;
using System.Runtime.InteropServices;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace JUnity.Services.Graphics
{
    internal sealed class ConstantBuffer<T> : IDisposable
        where T : struct
    {
        private readonly Device _device;
        private readonly Buffer _buffer;

        public ConstantBuffer(Device device)
        {
            _device = device;

            _buffer = new Buffer(device, new BufferDescription
            {
                Usage = ResourceUsage.Dynamic,
                BindFlags = BindFlags.ConstantBuffer,
                SizeInBytes = Marshal.SizeOf<T>(),
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            });
        }

        public Buffer Buffer { get => _buffer; }

        public void Update(T value)
        {
            _device.ImmediateContext.MapSubresource(_buffer, MapMode.WriteDiscard, MapFlags.None, out var dataStream);
            dataStream.Write(value);
            _device.ImmediateContext.UnmapSubresource(_buffer, 0);
        }

        public void Dispose()
        {
            _buffer?.Dispose();
        }
    }
}

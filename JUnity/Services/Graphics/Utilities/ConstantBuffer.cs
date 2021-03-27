using System;
using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace JUnity.Services.Graphics.Utilities
{
    internal sealed class ConstantBuffer<T> : IDisposable
        where T : struct
    {
        private readonly Device _device;
        private readonly Buffer _buffer;
        //private readonly DataStream _dataStream;

        public ConstantBuffer(Device device)
        {
            _device = device;

            var size = Marshal.SizeOf<T>();
            _buffer = new Buffer(device, new BufferDescription
            {
                Usage = ResourceUsage.Dynamic,
                BindFlags = BindFlags.ConstantBuffer,
                SizeInBytes = size,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            });

            //_dataStream = new DataStream(size, true, true);
        }

        public Buffer Buffer { get => _buffer; }

        public void Update(T value)
        {
            _device.ImmediateContext.MapSubresource(_buffer, MapMode.WriteDiscard, MapFlags.None, out var dataStream);
            Marshal.StructureToPtr(value, dataStream.DataPointer, false);
            _device.ImmediateContext.UnmapSubresource(_buffer, 0);

            //Marshal.StructureToPtr(value, _dataStream.DataPointer, false);

            //var dataBox = new DataBox(_dataStream.DataPointer, 0, 0);
            //_device.ImmediateContext.UpdateSubresource(dataBox, _buffer, 0);
        }

        public void Dispose()
        {
            //_dataStream?.Dispose();
            _buffer?.Dispose();
        }
    }
}

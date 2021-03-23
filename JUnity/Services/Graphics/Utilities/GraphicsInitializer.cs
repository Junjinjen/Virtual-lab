using System;
using System.Linq;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using System.Collections.Generic;
using Device = SharpDX.Direct3D11.Device;
using System.Xml.Linq;
using SharpDX.D3DCompiler;
using System.IO;

namespace JUnity.Services.Graphics.Utilities
{
    internal static class GraphicsInitializer
    {
        public static void CreateDeviceWithSwapChain(GraphicsSettings graphicsSettings, RenderForm renderForm, SampleDescription sampleDescription,
            out SwapChainDescription swapChainDescription, out SwapChain swapChain, out Device device)
        {
            swapChainDescription = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription = new ModeDescription(renderForm.ClientSize.Width,
                    renderForm.ClientSize.Height,
                    new Rational(graphicsSettings.Numerator, graphicsSettings.Denominator),
                    Format.R8G8B8A8_UNorm),
                IsWindowed = graphicsSettings.IsWindowed,
                OutputHandle = renderForm.Handle,
                SampleDescription = sampleDescription,
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

#if DEBUG
            Configuration.EnableObjectTracking = true;
#endif

            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, swapChainDescription, out device, out swapChain);
        }

        public static void InitializeShaders(string shadersMetaPath, out ShaderSignature inputSignature,
            out Dictionary<string, VertexShader> vertexShaders, out Dictionary<string, PixelShader> pixelShaders)
        {
            var document = XDocument.Load(shadersMetaPath);

            var vertexShadersInfo = from shader in document.Element("shaders").Elements("vertex-shader")
                                    select new ShaderInfo
                                    {
                                        Name = shader.Element("name").Value,
                                        FileName = shader.Element("filename").Value,
                                        EntryPoint = shader.Element("entry-point").Value
                                    };

            var pixelShadersInfo = from shader in document.Element("shaders").Elements("pixel-shader")
                                   select new ShaderInfo
                                   {
                                       Name = shader.Element("name").Value,
                                       FileName = shader.Element("filename").Value,
                                       EntryPoint = shader.Element("entry-point").Value
                                   };

            vertexShaders = new Dictionary<string, VertexShader>();
            pixelShaders = new Dictionary<string, PixelShader>();

            inputSignature = null;
            var shadersFolder = Path.GetDirectoryName(shadersMetaPath);

            foreach (var shaderInfo in vertexShadersInfo)
            {
                var vertexShaderByteCode = ShaderBytecode.CompileFromFile(Path.Combine(shadersFolder, shaderInfo.FileName), shaderInfo.EntryPoint, "vs_5_0", include: new IncludeHandler(shadersFolder));
                var vertexShader = new VertexShader(Engine.Instance.GraphicsRenderer.Device, vertexShaderByteCode);

                vertexShaders.Add(shaderInfo.Name, vertexShader);

                var shaderSignature = ShaderSignature.GetInputSignature(vertexShaderByteCode);
                if (inputSignature == null)
                {
                    inputSignature = shaderSignature;
                }
                else if (!shaderSignature.Data.SequenceEqual(inputSignature.Data))
                {
                    throw new Exception("Vertex shaders have different input signatures.");
                }

                vertexShaderByteCode.Dispose();
            }

            foreach (var shaderInfo in pixelShadersInfo)
            {
                var pixelShaderByteCode = ShaderBytecode.CompileFromFile(Path.Combine(shadersFolder, shaderInfo.FileName), shaderInfo.EntryPoint, "ps_5_0", include: new IncludeHandler(shadersFolder));
                var pixelShader = new PixelShader(Engine.Instance.GraphicsRenderer.Device, pixelShaderByteCode);

                pixelShaders.Add(shaderInfo.Name, pixelShader);

                pixelShaderByteCode.Dispose();
            }
        }

        public static SamplerState CreateSamplerState(TextureSampling textureSampling)
        {
            var desc = new SamplerStateDescription
            {
                AddressU = TextureAddressMode.Clamp,
                AddressV = TextureAddressMode.Clamp,
                AddressW = TextureAddressMode.Clamp,
                MipLodBias = 0,
                MaximumAnisotropy = 16,
                ComparisonFunction = Comparison.Never,
                BorderColor = new SharpDX.Mathematics.Interop.RawColor4(1.0f, 1.0f, 1.0f, 1.0f),
                MinimumLod = 0,
                MaximumLod = float.MaxValue
            };

            switch (textureSampling)
            {
                case TextureSampling.Point:
                    desc.Filter = Filter.MinMagMipPoint;
                    break;
                case TextureSampling.Linear:
                    desc.Filter = Filter.MinMagMipLinear;
                    break;
                case TextureSampling.Anisotropic:
                    desc.Filter = Filter.Anisotropic;
                    break;
                default:
                    throw new ArgumentException("Texture sampling option not found.", nameof(textureSampling));
            }

            return new SamplerState(Engine.Instance.GraphicsRenderer.Device, desc);
        }

        public static RasterizerState CreateRasterizerStage()
        {
            var desc = new RasterizerStateDescription
            {
                CullMode = CullMode.Back,
                FillMode = FillMode.Solid,
                IsFrontCounterClockwise = true,
                IsMultisampleEnabled = true,
                IsAntialiasedLineEnabled = true,
                IsDepthClipEnabled = true,
            };

            return new RasterizerState(Engine.Instance.GraphicsRenderer.Device, desc);
        }

        private class ShaderInfo
        {
            public string Name { get; set; }

            public string FileName { get; set; }

            public string EntryPoint { get; set; }
        }
    }
}

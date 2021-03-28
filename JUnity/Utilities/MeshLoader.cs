using Assimp;
using JUnity.Services.Graphics.Meshing;
using SharpDX;
using System.Collections.Generic;
using JunityMesh = JUnity.Services.Graphics.Meshing.Mesh;
using JunityMaterial = JUnity.Services.Graphics.Meshing.Material;
using JUnity.Services.Graphics;
using System.Linq;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace JUnity.Utilities
{
    public static class MeshLoader
    {
        public static int MipLevels { get; set; } = 8;

        public static NodeCollection LoadScene(string filename)
        {
            var meshCollection = new NodeCollection();

            var context = new AssimpContext();
            var scene = context.ImportFile(filename, PostProcessPreset.TargetRealTimeMaximumQuality);
            foreach (var node in scene.RootNode.Children)
            {
                var meshDesc = CreateDescriptionFromNode(node, scene);
                meshCollection.Add(meshDesc);
            }

            return meshCollection;
        }

        private static NodeDescription CreateDescriptionFromNode(Node node, Scene scene)
        {
            var desc = new NodeDescription
            {
                Name = node.Name,
            };

            if (node.HasMeshes)
            {
                foreach (var index in node.MeshIndices)
                {
                    desc.NodeMeshes.Add(LoadMeshFromScene(index, scene));
                }
            }

            if (node.HasChildren)
            {
                foreach (var child in node.Children)
                {
                    desc.Children.Add(CreateDescriptionFromNode(child, scene));
                }
            }

            return desc;
        }

        private static JunityMesh LoadMeshFromScene(int index, Scene scene)
        {
            var nodeMesh = scene.Meshes[index];
            var vertices = new VertexDescription[nodeMesh.VertexCount];

            for (int i = 0; i < nodeMesh.VertexCount; i++)
            {
                vertices[i] = new VertexDescription
                {
                    Position = ToSharpDXVector(nodeMesh.Vertices[i]),
                };

                if (nodeMesh.HasNormals)
                {
                    vertices[i].Normal = ToSharpDXVector(nodeMesh.Normals[i]);
                }

                if (nodeMesh.HasTextureCoords(0))
                {
                    vertices[i].TextureCoordinate = ToSharpDXTexCoord(nodeMesh.TextureCoordinateChannels[0][i]);
                }

                if (nodeMesh.HasVertexColors(0))
                {
                    vertices[i].Color = ToSharpDXColor(nodeMesh.VertexColorChannels[0][i]);
                }
                else
                {
                    vertices[i].Color = Color4.White;
                }
            }

            var material = LoadMaterialFromIndex(nodeMesh.MaterialIndex, scene);

            SharpDX.Direct3D.PrimitiveTopology primitiveType = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
            switch (nodeMesh.PrimitiveType)
            {
                case PrimitiveType.Point:
                    primitiveType = SharpDX.Direct3D.PrimitiveTopology.PointList;
                    break;
                case PrimitiveType.Line:
                    primitiveType = SharpDX.Direct3D.PrimitiveTopology.LineList;
                    break;
                case PrimitiveType.Triangle:
                    primitiveType = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
                    break;
                case PrimitiveType.Polygon:
                    throw new System.NotImplementedException("PrimitiveType.Polygon not implemented yet");
            }

            return new JunityMesh(vertices, nodeMesh.GetUnsignedIndices(), material, primitiveType);
        }

        private static JunityMaterial LoadMaterialFromIndex(int index, Scene scene)
        {
            var nodeMaterial = scene.Materials[index];
            var answ = new JunityMaterial();

            if (nodeMaterial.HasColorAmbient)
            {
                answ.AmbientCoefficient = ToMaterialCoefficient(nodeMaterial.ColorAmbient);
            }

            if (nodeMaterial.HasColorDiffuse)
            {
                answ.DiffusionCoefficient = ToMaterialCoefficient(nodeMaterial.ColorDiffuse);
            }

            if (nodeMaterial.HasColorEmissive)
            {
                answ.EmissivityCoefficient = ToMaterialCoefficient(nodeMaterial.ColorEmissive);
            }

            if (nodeMaterial.HasColorSpecular)
            {
                answ.SpecularCoefficient = ToMaterialCoefficient(nodeMaterial.ColorSpecular);
                answ.SpecularPower = nodeMaterial.ShininessStrength;
            }

            if (nodeMaterial.HasTextureDiffuse)
            {
                if (nodeMaterial.TextureDiffuse.FilePath[0] == '*')
                {
                    answ.Texture = LoadDiffuseTextureByIndex(nodeMaterial.TextureDiffuse.TextureIndex, scene);
                }
                else
                {
                    answ.Texture = LoadDiffuseTextureByPath(nodeMaterial.TextureDiffuse.FilePath);
                }
            }

            return answ;
        }

        private static Texture LoadDiffuseTextureByIndex(int index, Scene scene)
        {
            var nodeTexture = scene.Textures[index];
            if (!nodeTexture.IsCompressed)
            {
                return CreateTextureFromTexelArray(nodeTexture.NonCompressedData, nodeTexture.Width, nodeTexture.Height);
            }
            else
            {
                using (var stream = new MemoryStream(nodeTexture.CompressedData))
                {
                    using (var image = (System.Drawing.Bitmap)System.Drawing.Image.FromStream(stream))
                    {
                        if (image.PixelFormat != PixelFormat.Format32bppArgb)
                        {
                            using (var rightFormatImage = image.Clone(new System.Drawing.Rectangle(0, 0, image.Width, image.Height), PixelFormat.Format32bppArgb))
                            {
                                return CreateTextureFromBitmapData(rightFormatImage);
                            }
                        }

                        return CreateTextureFromBitmapData(image);
                    }
                }
            }
        }

        private static Texture CreateTextureFromBitmapData(System.Drawing.Bitmap bitmap)
        {
            var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var length = bitmapData.Stride * bitmapData.Height;

            var bytes = new byte[length];

            Marshal.Copy(bitmapData.Scan0, bytes, 0, length);
            bitmap.UnlockBits(bitmapData);

            var colors = new Color4[bitmap.Width * bitmap.Height];
            int j = 0;
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = new Color4(bytes[j++], bytes[j++], bytes[j++], bytes[j++]);
            }

            return new Texture(colors, bitmap.Width, bitmap.Height, MipLevels);
        }

        private static Texture CreateTextureFromTexelArray(Texel[] data, int width, int height)
        {
            var sharpdxData = data.Select(x => new Color4(x.R, x.G, x.B, x.A)).ToArray();
            return new Texture(sharpdxData, width, height, MipLevels);
        }

        private static Texture LoadDiffuseTextureByPath(string filename)
        {
            return new Texture(filename, MipLevels);
        }

        private static Vector4 ToSharpDXVector(Vector3D vector)
        {
            return new Vector4
            {
                X = vector.X,
                Y = vector.Y,
                Z = vector.Z,
                W = 1,
            };
        }

        private static Vector3 ToMaterialCoefficient(Color4D color)
        {
            return new Vector3(color.R, color.G, color.B);
        }

        private static Color4 ToSharpDXColor(Color4D color)
        {
            return new Color4(color.R, color.G, color.B, color.A);
        }

        private static Vector2 ToSharpDXTexCoord(Vector3D coord)
        {
            return new Vector2(coord.X, coord.Y);
        }
    }
}

using Assimp;
using JUnity.Services.Graphics.Meshing;
using SharpDX;
using JunityMesh = JUnity.Services.Graphics.Meshing.Mesh;
using JunityMaterial = JUnity.Services.Graphics.Meshing.Material;
using JUnity.Services.Graphics;
using System.Linq;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace JUnity.Utilities
{
    public static class MeshLoader
    {
        private static readonly Regex _textureNumberRegex = new Regex(@"^\*(\d+)$");

        public static NodeCollection LoadScene(string filename, int mipLevels = 8)
        {
            var meshCollection = new NodeCollection();

            var context = new AssimpContext();
            var scene = context.ImportFile(filename, PostProcessPreset.TargetRealTimeMaximumQuality);

            foreach (var node in scene.RootNode.Children)
            {
                var meshDesc = CreateDescriptionFromNode(node, scene, mipLevels);
                meshCollection.Add(meshDesc);
            }

            return meshCollection;
        }

        private static NodeDescription CreateDescriptionFromNode(Node node, Scene scene, int mipLevels)
        {
            node.Transform.Decompose(out var scale, out var rotation, out var position);

            var desc = new NodeDescription
            {
                Name = node.Name,
                Position = ToSharpDXVector3(position).ToLeftHanded(),
                Rotation = ToSharpDXQuaternion(rotation).ToLeftHanded(),
                Scale = Vector3.One,
            };

            if (node.HasMeshes)
            {
                foreach (var index in node.MeshIndices)
                {
                    desc.NodeMeshes.Add(LoadMeshFromScene(index, scene, mipLevels, ToSharpDXVector3(scale).ToLeftHandedScale()));
                }
            }

            if (node.HasChildren)
            {
                foreach (var child in node.Children)
                {
                    desc.Children.Add(CreateDescriptionFromNode(child, scene, mipLevels));
                }
            }

            return desc;
        }

        private static JunityMesh LoadMeshFromScene(int index, Scene scene, int mipLevels, Vector4 scale)
        {
            var nodeMesh = scene.Meshes[index];
            var material = LoadMaterialFromIndex(nodeMesh.MaterialIndex, scene, mipLevels, out var opacity, out var color);
            var vertices = new VertexDescription[nodeMesh.VertexCount];

            for (int i = 0; i < nodeMesh.VertexCount; i++)
            {
                vertices[i] = new VertexDescription
                {
                    Position = ToSharpDXVector(nodeMesh.Vertices[i]).ToLeftHanded() * scale,
                };

                //if (nodeMesh.HasNormals)
                //{
                //    vertices[i].Normal = ToSharpDXVector(nodeMesh.Normals[i]).ToLeftHandedNormal();
                //}

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
                    vertices[i].Color = new Color4(color.Red, color.Green, color.Blue, opacity);
                }
            }

            var indices = nodeMesh.GetUnsignedIndices();

            for (int i = 0; i < indices.Length; i += 3)
            {
                var point1 = vertices[indices[i]];
                var point2 = vertices[indices[i + 1]];
                var point3 = vertices[indices[i + 2]];

                var vector1 = point2.Position - point1.Position;
                var vector2 = point3.Position - point1.Position;

                var normal = Vector3.Cross(new Vector3(vector1.X, vector1.Y, vector1.Z), new Vector3(vector2.X, vector2.Y, vector2.Z));
                normal.Normalize();
                var norm = -new Vector4(normal, 1);

                vertices[indices[i]].Normal = norm;
                vertices[indices[i + 1]].Normal = norm;
                vertices[indices[i + 2]].Normal = norm;
            }

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

            return new JunityMesh(vertices, indices, material, primitiveType);
        }

        private static JunityMaterial LoadMaterialFromIndex(int index, Scene scene, int mipLevels, out float opacity, out Color4 color)
        {
            var nodeMaterial = scene.Materials[index];
            opacity = nodeMaterial.Opacity;
            var answ = new JunityMaterial();

            answ.AmbientCoefficient = Color4.White;
            answ.DiffusionCoefficient = Color4.White;
            answ.SpecularCoefficient = Color4.White;
            answ.SpecularPower = 1;
            
            if (nodeMaterial.HasTextureDiffuse)
            {
                color = Color4.White;
                var match = _textureNumberRegex.Match(nodeMaterial.TextureDiffuse.FilePath);
                if (match.Success)
                {
                    answ.Texture = LoadDiffuseTextureByIndex(int.Parse(match.Groups[1].Value), scene, mipLevels);
                }
                else
                {
                    answ.Texture = LoadDiffuseTextureByPath(nodeMaterial.TextureDiffuse.FilePath, mipLevels);
                }
            }
            else
            {
                color = ToSharpDXColor(nodeMaterial.ColorDiffuse);
            }

            if (opacity < 1f)
            {
                answ.CullMode = SharpDX.Direct3D11.CullMode.None;
            }

            return answ;
        }

        private static Texture LoadDiffuseTextureByIndex(int index, Scene scene, int mipLevels)
        {
            var nodeTexture = scene.Textures[index];
            if (!nodeTexture.IsCompressed)
            {
                return CreateTextureFromTexelArray(nodeTexture.NonCompressedData, nodeTexture.Width, nodeTexture.Height, mipLevels);
            }
            else
            {
                using (var stream = new MemoryStream(nodeTexture.CompressedData))
                {
                    using (var image = new System.Drawing.Bitmap(stream))
                    {
                        if (image.PixelFormat != PixelFormat.Format32bppArgb)
                        {
                            using (var rightFormatImage = image.Clone(new System.Drawing.Rectangle(0, 0, image.Width, image.Height), PixelFormat.Format32bppArgb))
                            {
                                return CreateTextureFromBitmapData(rightFormatImage, mipLevels);
                            }
                        }

                        return CreateTextureFromBitmapData(image, mipLevels);
                    }
                }
            }
        }

        private static Texture CreateTextureFromBitmapData(System.Drawing.Bitmap bitmap, int mipLevels)
        {
            bitmap.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY);
            var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var length = bitmapData.Stride * bitmapData.Height;

            var bytes = new byte[length];

            Marshal.Copy(bitmapData.Scan0, bytes, 0, length);
            bitmap.UnlockBits(bitmapData);

            var colors = new Color[bitmap.Width * bitmap.Height];
            int j = 0;
            byte a, r, g, b;
            for (int i = 0; i < colors.Length; i++)
            {
                b = bytes[j++];
                g = bytes[j++];
                r = bytes[j++];
                a = bytes[j++];

                colors[i] = new Color(r, g, b, a);
            }

            return new Texture(colors, bitmap.Width, bitmap.Height, mipLevels);
        }

        private static Texture CreateTextureFromTexelArray(Texel[] data, int width, int height, int mipLevels)
        {
            var sharpdxData = data.Select(x => new Color(x.R, x.G, x.B, x.A)).ToArray();
            return new Texture(sharpdxData, width, height, mipLevels);
        }

        private static Texture LoadDiffuseTextureByPath(string filename, int mipLevels)
        {
            return new Texture(filename, mipLevels);
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

        private static SharpDX.Quaternion ToSharpDXQuaternion(Assimp.Quaternion quaternion)
        {
            return new SharpDX.Quaternion
            {
                W = quaternion.W,
                X = quaternion.X,
                Y = quaternion.Y,
                Z = quaternion.Z,
            };
        }

        private static Vector3 ToSharpDXVector3(Vector3D vector)
        {
            return new Vector3
            {
                X = vector.X,
                Y = vector.Y,
                Z = vector.Z,
            };
        }

        private static Color4 ToSharpDXColor(Color4D color)
        {
            return new Color4(color.R, color.G, color.B, color.A);
        }

        private static Vector2 ToSharpDXTexCoord(Vector3D coord)
        {
            return new Vector2(coord.X, coord.Y);
        }

        private static Vector4 ToLeftHandedScale(this Vector3 vector)
        {
            return new Vector4(vector.X, vector.Z, vector.Y, 1);
        }

        private static Vector3 ToLeftHanded(this Vector3 vector)
        {
            return new Vector3(vector.X, vector.Y, -vector.Z);
        }

        private static Vector4 ToLeftHanded(this Vector4 vector)
        {
            var vector3 = new Vector3(vector.X, vector.Y, vector.Z);
            return new Vector4(vector3.ToLeftHanded(), vector.W);
        }

        private static SharpDX.Quaternion ToLeftHanded(this SharpDX.Quaternion quaternion)
        {
            return new SharpDX.Quaternion(quaternion.X, quaternion.Y, -quaternion.Z, -quaternion.W);
        }
    }
}

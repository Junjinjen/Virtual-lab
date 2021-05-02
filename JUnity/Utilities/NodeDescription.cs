using JUnity.Services.Graphics.Meshing;
using SharpDX;
using System.Collections.Generic;

namespace JUnity.Utilities
{
    public sealed class NodeDescription
    {
        public NodeDescription()
        {
            Children = new NodeCollection();
            NodeMeshes = new List<Mesh>();
        }

        public string Name { get; internal set; }

        public Vector3 Position { get; set; }

        public Quaternion Rotation { get; set; }

        public Vector3 Scale { get; set; }

        public List<Mesh> NodeMeshes { get; }

        public NodeCollection Children { get; }
    }
}

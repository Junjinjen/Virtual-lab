using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JUnity.Utilities
{
    public sealed class NodeCollection : ICollection<NodeDescription>
    {
        private readonly List<NodeDescription> _meshes = new List<NodeDescription>();

        public int Count => _meshes.Count;

        public bool IsReadOnly => false;

        public NodeDescription this[int index]
        {
            get
            {
                return _meshes[index];
            }
        }

        public NodeDescription this[string name] { get => _meshes.First(x => x.Name == name); }

        public NodeDescription FindInHierarchy(string name)
        {
            var mesh = _meshes.FirstOrDefault(x => x.Name == name);
            if (mesh != null)
            {
                return mesh;
            }

            foreach (var obj in _meshes)
            {
                var tmp = obj.Children.FindInHierarchy(name);
                if (tmp != null)
                {
                    return tmp;
                }
            }

            return null;
        }

        public void Add(NodeDescription item)
        {
            _meshes.Add(item);
        }

        public void Clear()
        {
            _meshes.Clear();
        }

        public bool Contains(NodeDescription item)
        {
            return _meshes.Contains(item);
        }

        public void CopyTo(NodeDescription[] array, int arrayIndex)
        {
            _meshes.CopyTo(array, arrayIndex);
        }

        public IEnumerator<NodeDescription> GetEnumerator()
        {
            return _meshes.GetEnumerator();
        }

        public bool Remove(NodeDescription item)
        {
            return _meshes.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

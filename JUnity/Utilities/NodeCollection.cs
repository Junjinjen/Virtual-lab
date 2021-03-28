using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JUnity.Utilities
{
    public sealed class NodeCollection : ICollection<NodeDescription>
    {
        private readonly List<NodeDescription> _meshes = new List<NodeDescription>();

        public int Count => _meshes.Count + _meshes.Sum(x => x.Children.Count);

        public bool IsReadOnly => false;

        public NodeDescription this[int index]
        {
            get
            {
                return _meshes[index];
            }
        }

        public NodeDescription this[string name]
        {
            get
            {
                var answ = Find(name);
                if (answ != null)
                {
                    return answ;
                }

                throw new ArgumentOutOfRangeException(nameof(name), "Mesh with given name wasn't found.");
            }
        }

        public NodeDescription Find(string name)
        {
            var mesh = _meshes.FirstOrDefault(x => x.Name == name);
            if (mesh != null)
            {
                return mesh;
            }

            foreach (var obj in _meshes)
            {
                var tmp = obj.Children.Find(name);
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
            if (_meshes.Contains(item))
            {
                return true;
            }

            foreach (var obj in _meshes)
            {
                if (obj.Children.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(NodeDescription[] array, int arrayIndex)
        {
            _meshes.CopyTo(array, arrayIndex);
        }

        public IEnumerator<NodeDescription> GetEnumerator()
        {
            foreach (var root in _meshes)
            {
                yield return root;
                foreach (var obj in root.Children)
                {
                    yield return obj;
                }
            }
        }

        public bool Remove(NodeDescription item)
        {
            if (_meshes.Remove(item))
            {
                return true;
            }

            foreach (var obj in _meshes)
            {
                if (obj.Children.Remove(item))
                {
                    return true;
                }
            }

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

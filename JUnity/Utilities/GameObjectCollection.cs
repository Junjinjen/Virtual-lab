using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JUnity.Utilities
{
    public sealed class GameObjectCollection : ICollection<GameObject>
    {
        private readonly List<GameObject> _objects = new List<GameObject>();
        private readonly List<GameObject> _removeQueue = new List<GameObject>();
        private readonly GameObject _owner;

        public GameObjectCollection(GameObject owner)
        {
            _owner = owner;
        }

        public int Count => _objects.Count + _objects.Sum(x => x.Children.Count);

        public bool IsReadOnly => false;

        public void Add(GameObject item)
        {
            _objects.Add(item);
            item.Parent = _owner;
        }

        public void Clear()
        {
            _objects.Clear();
        }

        public GameObject this[string name]
        {
            get
            {
                var answ = Find(name);
                if (answ != null)
                {
                    return answ;
                }

                throw new ArgumentOutOfRangeException(nameof(name), "Object with given name wasn't found.");
            }
        }

        public GameObject Find(string name)
        {
            var gameObject = _objects.FirstOrDefault(x => x.Name == name);
            if (gameObject != null)
            {
                return gameObject;
            }

            foreach (var obj in _objects)
            {
                var tmp = obj.Children.Find(name);
                if (tmp != null)
                {
                    return tmp;
                }
            }

            return null;
        }

        public bool Contains(GameObject item)
        {
            if (_objects.Contains(item))
            {
                return true;
            }

            foreach (var obj in _objects)
            {
                if (obj.Children.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(GameObject[] array, int arrayIndex)
        {
            _objects.CopyTo(array, arrayIndex);
        }

        public IEnumerator<GameObject> GetEnumerator()
        {
            foreach (var root in _objects)
            {
                yield return root;
                foreach (var obj in root.Children)
                {
                    yield return obj;
                }
            }
        }

        public bool Remove(GameObject item)
        {
            if (_objects.Contains(item))
            {
                _removeQueue.Add(item);
                return true;
            }

            foreach (var obj in _objects)
            {
                if (obj.Children.Remove(item))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Remove(string name)
        {
            var objectToRemove = _objects.FirstOrDefault(x => x.Name == name);
            if (objectToRemove != null)
            {
                _removeQueue.Add(objectToRemove);
                return true;
            }

            foreach (var obj in _objects)
            {
                if (obj.Children.Remove(name))
                {
                    return true;
                }
            }

            return false;
        }

        internal void CommitRemove()
        {
            _objects.RemoveAll(x => _removeQueue.Contains(x));
            _removeQueue.Clear();

            foreach (var obj in _objects)
            {
                obj.Children.CommitRemove();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

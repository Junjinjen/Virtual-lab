using System.Collections;
using System.Collections.Generic;

namespace JUnity.Utilities
{
    public class GameObjectReadOnlyCollection : IReadOnlyList<GameObject>
    {
        protected readonly List<GameObject> _objects = new List<GameObject>();

        public GameObject this[int index] => _objects[index];

        public GameObject this[string name] => _objects.Find(x => x.Name == name);

        public int Count => _objects.Count;

        public IEnumerator<GameObject> GetEnumerator()
        {
            return _objects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _objects.GetEnumerator();
        }
    }
}

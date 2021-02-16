using System.Collections.Generic;

namespace JUnity.Utilities
{
    public sealed class GameObjectCollection : GameObjectReadOnlyCollection
    {
        private readonly List<GameObject> _removeQueue = new List<GameObject>();

        public void AddObject(GameObject @object)
        {
            _objects.Add(@object);
        }

        internal void CommitRemove()
        {
            _objects.RemoveAll(x => _removeQueue.Contains(x));
            _removeQueue.Clear();
        }

        public void RequestRemove(GameObject @object)
        {
            _removeQueue.Add(@object);
        }

        public void RequestRemove(int index)
        {
            _removeQueue.Add(_objects[index]);
        }

        public void RequestRemove(string name)
        {
            _removeQueue.Add(_objects.Find(x => x.Name == name));
        }
    }
}

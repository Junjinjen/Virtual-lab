using JUnity.Components;
using JUnity.Utilities;
using SharpDX;
using System.Collections.Generic;

namespace JUnity
{
    public abstract class GameObject
    {
        private const string _defaultName = "Unnamed object";
        private readonly List<GameComponent> _components = new List<GameComponent>();
        private readonly List<GameComponent> _fixedComponents = new List<GameComponent>();

        protected GameObject()
            : this(_defaultName)
        {
        }

        protected GameObject(string name)
            : this(name, null)
        {
        }

        protected GameObject(GameObject parent)
            : this(_defaultName, parent)
        {
        }

        protected GameObject(string name, GameObject parent)
        {
            Name = name;
            Parent = parent;
            IsActive = true;
        }

        internal void OnUpdate(double deltaTime)
        {
            Update(deltaTime);
            foreach (var component in _components)
            {
                component.CallComponent(deltaTime);
            }
        }

        internal void OnFixedUpdate(double deltaTime)
        {
            FixedUpdate(deltaTime);
            foreach (var component in _fixedComponents)
            {
                component.CallComponent(deltaTime);
            }

            foreach (var component in _components)
            {
                component.CallComponent(deltaTime);
            }
        }

        public bool IsActive { get; set; }

        public string Name { get; protected set; }

        public GameObject Parent { get; }

        public GameObjectReadOnlyCollection Children { get; }

        public Rotation Rotation { get; set; }

        public Vector3 Position { get; set; }

        public abstract void Start();

        public abstract void Update(double deltaTime);

        public abstract void FixedUpdate(double deltaTime);

        public void Destroy()
        {
            JUnity.Instance.Scene.RequestRemove(this);
        }
    }
}

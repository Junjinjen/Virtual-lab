using Engine.Components;
using Engine.Utilities;
using SharpDX;
using System.Collections.Generic;

namespace Engine
{
    public abstract class GameObject
    {
        private const string DefaultName = "Unnamed object";
        private readonly List<GameComponent> _components = new List<GameComponent>();
        private readonly List<GameComponent> _fixedComponents = new List<GameComponent>();

        protected GameObject()
            : this(DefaultName)
        {
        }

        protected GameObject(string name)
        {
            Name = name;
            IsActive = true;
            Children = new GameObjectCollection(this);
        }

        public bool IsActive { get; set; }

        public string Name { get; protected set; }

        public GameObject Parent { get; internal set; }

        public GameObjectCollection Children { get; }

        public Quaternion Rotation { get; set; }

        public Vector3 Position { get; set; }

        public virtual void Start() { }

        public virtual void Update(double deltaTime) { }

        public virtual void FixedUpdate(double deltaTime) { }

        public void Destroy()
        {
            JUnity.Instance.Scene.Remove(this);
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
        }
    }
}

using JUnity.Components;
using JUnity.Utilities;
using SharpDX;
using System.Collections.Generic;

namespace JUnity
{
    public sealed class GameObject
    {
        private const string DefaultName = "Unnamed object";
        private readonly List<GameComponent> _components = new List<GameComponent>();
        private readonly List<GameComponent> _fixedComponents = new List<GameComponent>();

        public GameObject()
            : this(DefaultName)
        {
        }

        public GameObject(string name)
        {
            Name = name;
            IsActive = true;
            Children = new GameObjectCollection(this);
        }

        public bool IsActive { get; set; }

        public string Name { get; }

        public Script Script { get; set; }

        public GameObject Parent { get; internal set; }

        public GameObjectCollection Children { get; }

        public Quaternion Rotation { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 Scale { get; set; } = Vector3.One;

        internal void OnUpdate(double deltaTime)
        {
            Script?.Update(deltaTime);
            foreach (var component in _components)
            {
                component.CallComponent(deltaTime);
            }
        }

        internal void OnFixedUpdate(double deltaTime)
        {
            Script?.FixedUpdate(deltaTime);
            foreach (var component in _fixedComponents)
            {
                component.CallComponent(deltaTime);
            }
        }
    }
}

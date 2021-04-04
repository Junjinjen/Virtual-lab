using System;
using System.Reflection;
using JUnity.Components;
using JUnity.Components.Interfaces;
using JUnity.Utilities;
using SharpDX;
using System.Collections.Generic;

namespace JUnity
{
    public sealed class GameObject
    {
        private const string DefaultName = "Unnamed object";
        private Script _script;
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
            LocalScale = Vector3.One;
            LocalRotation = Quaternion.Identity;
        }

        public TComponent AddComponent<TComponent>()
            where TComponent : GameComponent
        {
            if (typeof(IUniqueComponent).IsAssignableFrom(typeof(TComponent)) && GetComponent<TComponent>() != null)
            {
                throw new InvalidOperationException("Unable to add unique component. Dublication detected.");
            }

            var component = CreateInstance<TComponent>();
            if (typeof(IFixedUpdatableComponent).IsAssignableFrom(typeof(TComponent)))
            {
                _fixedComponents.Add(component);
            }
            else
            {
                _components.Add(component);
            }

            return component;
        }

        public TScript AddScript<TScript>()
            where TScript : Script
        {
            if (_script != null)
            {
                throw new InvalidOperationException("Unable to add script component. Dublication detected.");
            }

            var script = CreateInstance<TScript>();
            _script = script;

            return script;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields",
            Justification = "Engine logic")]
        private TComponent CreateInstance<TComponent>()
        {
            var ctor = typeof(TComponent).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                            null, new[] { typeof(GameObject) }, null);

            var component = (TComponent)ctor.Invoke(new[] { this });
            return component;
        }

        public List<TComponent> GetComponents<TComponent>()
            where TComponent : GameComponent
        {
            var answ = new List<TComponent>();
            foreach (var item in _components)
            {
                if (item is TComponent component)
                {
                    answ.Add(component);
                }
            }

            foreach (var item in _fixedComponents)
            {
                if (item is TComponent component)
                {
                    answ.Add(component);
                }
            }

            return answ;
        }

        public TComponent GetComponent<TComponent>()
            where TComponent : GameComponent
        {
            var answ = SearchComponent<TComponent>(_components);
            if (answ == null)
            {
                answ = SearchComponent<TComponent>(_fixedComponents);
            }

            return answ;
        }

        private TComponent SearchComponent<TComponent>(List<GameComponent> components)
            where TComponent : GameComponent
        {
            foreach (var item in components)
            {
                if (item is TComponent component)
                {
                    return component;
                }
            }

            return null;
        }  
        
        public bool IsActive { get; set; }

        public string Name { get; }

        public GameObject Parent { get; internal set; }

        public GameObjectCollection Children { get; }

        public Vector3 LocalPosition { get; set; }

        public Quaternion LocalRotation { get; set; }

        public Vector3 LocalScale { get; set; }

        internal Quaternion Rotation { get => Parent != null ?  Quaternion.Multiply(Parent.Rotation, LocalRotation) : LocalRotation; }

        internal Vector3 Position { get => Parent != null ?  Vector3.Transform(LocalPosition, Parent.Rotation) + Parent.Position : LocalPosition; }

        internal Vector3 Scale { get => Parent != null?  LocalScale * Parent.Scale : LocalScale; }

        internal void OnStartup()
        {
            _script?.Start();
            foreach (var component in _components)
            {
                component.Start();
            }

            foreach (var component in _fixedComponents)
            {
                component.Start();
            }
        }

        internal void OnUpdate(double deltaTime)
        {
            _script?.Update(deltaTime);
            foreach (var component in _components)
            {
                component.CallComponent(deltaTime);
            }
        }

        internal void OnFixedUpdate(double deltaTime)
        {
            _script?.FixedUpdate(deltaTime);
            foreach (var component in _fixedComponents)
            {
                component.CallComponent(deltaTime);
            }
        }
    }
}

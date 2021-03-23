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
            Scale = Vector3.One;
        }

        public TComponent AddComponent<TComponent>()
            where TComponent : GameComponent
        {
            if (typeof(IUniqueComponent).IsAssignableFrom(typeof(TComponent)) && GetComponent<TComponent>() != null)
            {
                throw new InvalidOperationException("Unable to add unique component. Dublication detected.");
            }

            var ctor = typeof(TComponent).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                null, new[] { typeof(GameObject) }, null);

            var component = (TComponent)ctor.Invoke(new[] { this });
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

        public Script Script { get; set; }

        public GameObject Parent { get; internal set; }

        public GameObjectCollection Children { get; }

        public Quaternion Rotation { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 Scale { get; set; }

        internal void OnStartup()
        {
            Script?.Start();
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

using System;
using System.Reflection;
using JUnity.Components;
using JUnity.Components.Interfaces;
using JUnity.Utilities;
using SharpDX;
using System.Collections.Generic;
using JUnity.Components.UI;

namespace JUnity
{
    public sealed class GameObject
    {
        private const string DefaultName = "Unnamed object";

        private Script _script;
        private readonly Canvas _canvas;

        private readonly List<GameComponent> _components = new List<GameComponent>();
        private readonly List<GameComponent> _fixedComponents = new List<GameComponent>();

        private Quaternion _rotationQuaternion;
        private Matrix _positionMatrix;
        private Matrix _rotationMatrix;
        private Matrix _scaleMatrix;

        private bool _isWorldMatrixRequairesRecompute;
        private Matrix _worldMatrix;

        public GameObject()
            : this(DefaultName)
        {
        }

        public GameObject(string name)
        {
            Name = name;
            IsActive = true;
            Children = new GameObjectCollection(this);
            _canvas = new Canvas();

            _positionMatrix = Matrix.Identity;
            _scaleMatrix = Matrix.Identity;

            Position = Vector3.Zero;
            Rotation = Quaternion.Identity;
            Scale = Vector3.One;
        }

        public event EventHandler<TranslationEventArgs> OnTranslation;
        public event EventHandler<RotationEventArgs> OnRotation;
        public event EventHandler<ScaleEventArgs> OnScale;

        public bool IsActive { get; set; }

        public string Name { get; }

        public Script Script { get => _script; }

        public Canvas Canvas { get => _canvas; }

        public GameObject Parent { get; internal set; }

        public GameObjectCollection Children { get; }

        public Vector3 Position
        {
            get => _positionMatrix.TranslationVector;
            set
            {
                _positionMatrix.TranslationVector = value;
                _isWorldMatrixRequairesRecompute = true;

                OnTranslation?.Invoke(this, new TranslationEventArgs
                {
                    Position = value,
                });
            }
        }

        public Quaternion Rotation
        {
            get => _rotationQuaternion;
            set
            {
                _rotationQuaternion = value;
                Matrix.RotationQuaternion(ref _rotationQuaternion, out _rotationMatrix);
                _isWorldMatrixRequairesRecompute = true;

                OnRotation?.Invoke(this, new RotationEventArgs
                {
                    Rotation = value,
                });
            }
        }

        public Vector3 Scale
        {
            get => _scaleMatrix.ScaleVector;
            set
            {
                _scaleMatrix.ScaleVector = value;
                _isWorldMatrixRequairesRecompute = true;

                OnScale?.Invoke(this, new ScaleEventArgs
                {
                    Scale = value,
                });
            }
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
        
        internal Matrix GetWorldMatrix()
        {
            if (_isWorldMatrixRequairesRecompute)
            {
                _worldMatrix = _scaleMatrix * _rotationMatrix * _positionMatrix;
                _isWorldMatrixRequairesRecompute = false;
            }

            if (Parent != null)
            {
                return _worldMatrix * Parent.GetWorldMatrix();
            }

            return _worldMatrix;
        }

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

        public void Destroy()
        {
            _script?.OnDestroy();
            _canvas.Dispose();

            foreach (var component in _components)
            {
                component.Dispose();
            }

            foreach (var component in _fixedComponents)
            {
                component.Dispose();
            }

            Engine.Instance.Scene.Remove(this);
        }
    }

    public sealed class TranslationEventArgs : EventArgs
    {
        public Vector3 Position { get; set; }
    }

    public sealed class RotationEventArgs : EventArgs
    {
        public Quaternion Rotation { get; set; }
    }

    public sealed class ScaleEventArgs : EventArgs
    {
        public Vector3 Scale { get; set; }
    }
}

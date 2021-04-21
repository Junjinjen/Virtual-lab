using JUnity.Components.UI;
using JUnity.Utilities;
using System.Collections.Generic;

namespace JUnity.Components
{
    public abstract class Script
    {
        public GameObject Object { get; internal set; }

        public GameObjectCollection Scene
        {
            get
            {
                return Engine.Instance.Scene;
            }
        }

        public Canvas Canvas { get => Object.Canvas; }

        public TComponent AddComponent<TComponent>()
            where TComponent : GameComponent
        {
            return Object.AddComponent<TComponent>();
        }

        public List<TComponent> GetComponents<TComponent>()
            where TComponent : GameComponent
        {
            return Object.GetComponents<TComponent>();
        }

        public TComponent GetComponent<TComponent>()
            where TComponent : GameComponent
        {
            return Object.GetComponent<TComponent>();
        }

        public virtual void Start() { }

        public virtual void Update(double deltaTime) { }

        public virtual void FixedUpdate(double deltaTime) { }

        public virtual void OnDestroy() { }
    }
}

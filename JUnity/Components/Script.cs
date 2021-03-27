using JUnity.Utilities;
using System.Collections.Generic;

namespace JUnity.Components
{
    public abstract class Script
    {
        protected Script(GameObject owner)
        {
            Object = owner;
            Canvas = new Canvas();
        }

        public GameObject Object { get; }

        public GameObjectCollection Scene
        {
            get
            {
                return Engine.Instance.Scene;
            }
        }

        public Canvas Canvas { get; }

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

        public void Destroy()
        {
            Engine.Instance.Scene.Remove(Object);
            Canvas.Dispose();
        }
    }
}

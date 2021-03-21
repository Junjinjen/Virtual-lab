using JUnity.Utilities;

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

namespace JUnity.Components
{
    public abstract class Script
    {
        protected Script(GameObject owner)
        {
            Object = owner;
        }

        public GameObject Object { get; }

        public virtual void Start() { }

        public virtual void Update(double deltaTime) { }

        public virtual void FixedUpdate(double deltaTime) { }

        public void Destroy()
        {
            Engine.Instance.Scene.Remove(Object);
        }
    }
}

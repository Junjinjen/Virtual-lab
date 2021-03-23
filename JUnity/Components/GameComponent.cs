namespace JUnity.Components
{
    public abstract class GameComponent
    {
        protected GameComponent(GameObject owner)
        {
            Owner = owner;
        }

        public GameObject Owner { get; }

        internal virtual void Start() { }

        internal abstract void CallComponent(double deltaTime);
    }
}

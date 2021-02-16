namespace JUnity.Components
{
    public abstract class GameComponent
    {
        protected GameComponent(GameObject owner)
        {
            Owner = owner;
        }

        public GameObject Owner { get; }

        public abstract void Initialize();

        public abstract void CallComponent(double deltaTime);
    }
}

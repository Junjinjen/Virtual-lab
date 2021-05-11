using JUnity;
using JUnity.Utilities;
using Lab2.Scripts;

namespace Lab2.GameObjects
{
    public class Timer_Object : IGameObjectCreator
    {
        public GameObject Create()
        {
            var gameObject = new GameObject("Timer");
            gameObject.AddScript<Timer_Script>();
            return gameObject;
        }
    }
}

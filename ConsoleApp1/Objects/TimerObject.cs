using App.Scripts;
using JUnity;
using JUnity.Utilities;

namespace App.Objects
{
    public class TimerObject : IGameObjectCreator
    {
        public GameObject Create()
        {
            GameObject gameObject = new GameObject("Timer");
            gameObject.AddScript<TimerScript>();
            return gameObject;
        }
    }
}

using JUnity;
using JUnity.Utilities;
using Lab3.Scripts.UI;

namespace Lab3.GameObjects.UI
{
    public class TimerUI : IGameObjectCreator
    {
        public GameObject Create()
        {
            var obj = new GameObject("TimerUI");
            obj.AddScript<TimerPanelUI>();
            return obj;
        }
    }
}

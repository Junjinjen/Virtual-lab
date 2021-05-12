using JUnity;
using JUnity.Utilities;
using Lab3.Scripts.UI;

namespace Lab3.GameObjects.UI
{
    public class WaterUI : IGameObjectCreator
    {
        public GameObject Create()
        {
            var obj = new GameObject("WaterUI");
            obj.AddScript<WaterPanelUI>();
            return obj;
        }

    }
}

using JUnity;
using JUnity.Utilities;
using Lab3.Scripts.UI;

namespace Lab3.GameObjects.UI
{
    public class ElectricalСircuitUI : IGameObjectCreator
    {
        public GameObject Create()
        {
            var obj = new GameObject("ElectricalСircuitUI");
            obj.AddScript<ElectricalСircuitPanelUI>();
            return obj;
        }
    }
}

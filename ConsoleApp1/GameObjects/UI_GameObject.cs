using JUnity;
using JUnity.Utilities;
using Lab2.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.GameObjects
{
    public class UI_GameObject : IGameObjectCreator
    {
        public GameObject Create()
        {
            var gameObject = new GameObject("UI");
            gameObject.AddScript<UI_Script>();
            return gameObject;
        }
    }
}

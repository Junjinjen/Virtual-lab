using JUnity;
using JUnity.Components.Lighning;
using JUnity.Utilities;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.GameObjects
{
    public class Light_Object : IGameObjectCreator
    {
        public GameObject Create()
        {
            var gameObject = new GameObject("Light");
            var light = gameObject.AddComponent<DirectionLight>();
            light.Color = Color3.White;
            light.Direction = new Vector3(-0.3f, -1, 1f);
            light.Direction.Normalize();

            return gameObject;
        }
    }
}

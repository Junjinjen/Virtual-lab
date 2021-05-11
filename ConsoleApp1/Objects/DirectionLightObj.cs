using JUnity;
using JUnity.Components.Lighning;
using JUnity.Utilities;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Objects
{
    public class DirectionLightObj : IGameObjectCreator
    {
        public GameObject Create()
        {
            GameObject gameObject = new GameObject("DirectionLight");
            var directionLight = gameObject.AddComponent<DirectionLight>();
            directionLight.Color = Color3.White;
            directionLight.Direction = new Vector3(-1, -1, 0.1f);
            directionLight.Direction.Normalize();
            return gameObject;
        }
    }
}

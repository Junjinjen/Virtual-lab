using JUnity;
using JUnity.Utilities;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.GameObjects
{
    public class Background : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/background.fbx";
            var obj = GameObjectFactory.Create(new FbxObjectCreator(file, "Background"));
            obj.Position = new Vector3(-1f, 1f, 8f);
            obj.Scale = Vector3.One * 1.7f;
            obj.Rotation = Quaternion.RotationYawPitchRoll(0, 0, 0);
            return obj;
        }
    }
}

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
    public class Table : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/table.fbx";
            var obj = GameObjectFactory.Create(new FbxObjectCreator(file, "Table"));
            obj.Position = new Vector3(0f, -3f, 0f);
            obj.Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);
            obj.Scale = Vector3.One * 1.5f;

            return obj;
        }
    }
}

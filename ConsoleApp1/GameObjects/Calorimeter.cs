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
    public class Calorimeter : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/calorimeter2.fbx";
            GameObject obj = GameObjectFactory.Create(new FbxObjectCreator(file, "Calorimeter"));
            obj.Position = new Vector3(-1.5f, -3f, 0.0f);
            obj.Scale = Vector3.One * 1.5f;
            file = @"Meshes/water.fbx";
            obj.Children.Add(GameObjectFactory.Create(new FbxObjectCreator(file, "WaterCalorimeter")));
            obj.Children[4].Position = new Vector3(0f, 1.8f, 0f);
            obj.Children[4].Scale = Vector3.One * 0.9f + Vector3.UnitZ * 5;
            obj.Children[4].Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);
            GameObject tmp = obj.Children[1];
            GameObject tmp2 = obj.Children[2];
            GameObject tmp3 = obj.Children[4];
            obj.Children[1] = tmp2;
            obj.Children[2] = tmp3;
            obj.Children[4] = tmp;
            return obj;
        }
    }
}

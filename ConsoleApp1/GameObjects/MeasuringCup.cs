using JUnity;
using JUnity.Utilities;
using SharpDX;

namespace Lab2.GameObjects
{
    public class MeasuringCup : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/measuringCup2.fbx";
            var obj = GameObjectFactory.Create(new FbxObjectCreator(file, "Cup"));
            obj.Position = new Vector3(-4.5f, -3f, 0f);
            obj.Scale = Vector3.One * 1.5f;
            obj.Rotation = Quaternion.RotationYawPitchRoll(MathUtil.Pi - 0.5f, 0, 0);
            file = @"Meshes/water.fbx";
            obj.Children.Add(GameObjectFactory.Create(new FbxObjectCreator(file, "Water")));
            obj.Children[3].Position = new Vector3(0f, 0f, 0f);
            obj.Children[3].Scale = Vector3.One;
            obj.Children[3].Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);
            GameObject tmp4 = obj.Children[0];
            GameObject tmp5 = obj.Children[2];
            GameObject tmp6 = obj.Children[3];
            obj.Children[0] = tmp5;
            obj.Children[2] = tmp6;
            obj.Children[3] = tmp4;

            return obj;
        }
    }
}

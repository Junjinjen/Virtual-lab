using JUnity;
using JUnity.Utilities;
using SharpDX;

namespace Lab2.GameObjects
{
    public class WaterThermometer : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/thermometer.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "WaterThermometer"));
            obj.Position = new Vector3(-7.2f, -3f, 0f);
            obj.Scale = Vector3.One / 50f;
            obj.Rotation = Quaternion.RotationYawPitchRoll(MathUtil.Pi - 0.7f, 0, 0);
            file = @"Meshes/column.fbx";
            obj.Children.Add(GameObjectFactory.Create(new FbxObjectCreator(file, "ColumnWater")));
            obj.Children[3].Position = new Vector3(10f, 80f, 0);
            obj.Children[3].Scale = Vector3.One * 100f + Vector3.UnitZ * 4600;
            obj.Children[3].Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);

            return obj;
        }
    }
}

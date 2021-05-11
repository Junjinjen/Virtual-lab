using JUnity;
using JUnity.Utilities;
using SharpDX;

namespace Lab2.GameObjects
{
    public class ObjectThermometer : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/thermometer.fbx";
            var obj = GameObjectFactory.Create(new FbxObjectCreator(file, "ObjectThermometer"));
            obj.Position = new Vector3(2.9f, -3f, 0.0f);
            obj.Scale = Vector3.One / 50f;
            obj.Rotation = Quaternion.RotationYawPitchRoll(MathUtil.Pi + 0.2f, 0, 0);
            file = @"Meshes/column.fbx";
            obj.Children.Add(GameObjectFactory.Create(new FbxObjectCreator(file, "ColumnObject")));
            obj.Children[3].Position = new Vector3(10f, 80f, 0);
            obj.Children[3].Scale = Vector3.One * 100f + Vector3.UnitZ * 4550;
            obj.Children[3].Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);

            return obj;
        }
    }
}

using JUnity;
using JUnity.Utilities;
using Lab3.Scripts.MutableObjects;
using SharpDX;

namespace Lab3.GameObjects.Items
{
    public class Thermometer : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/thermometer.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Thermometer"));
            obj.Position = new Vector3(-3.5f, 0.0f, -5.0f);
            obj.Rotation *= Quaternion.RotationAxis(Vector3.UnitZ, MathUtil.Pi / 6);
            obj.Children[2].AddScript<TemparatureScript>();

            return obj;
        }
    }
}

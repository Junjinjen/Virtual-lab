using JUnity;
using JUnity.Utilities;
using Lab3.Scripts.MutableObjects;
using SharpDX;

namespace Lab3.GameObjects.Items
{
    public class Ammeter : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/ammeter.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Ammeter"));
            obj.Position = new Vector3(-4.5f, -2.0f, 2.5f);
            obj.Rotation *= Quaternion.RotationAxis(Vector3.UnitZ, MathUtil.Pi / 8);
            obj.Scale *= 1.4f;
            obj.Children[0].AddScript<AmmeterScript>();

            return obj;
        }
    }
}

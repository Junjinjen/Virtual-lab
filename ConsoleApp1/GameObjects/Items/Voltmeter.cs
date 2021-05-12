using JUnity;
using JUnity.Utilities;
using Lab3.Scripts.MutableObjects;
using SharpDX;

namespace Lab3.GameObjects.Items
{
    public class Voltmeter : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/voltmeter.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Voltmeter"));
            obj.Position = new Vector3(0.0f, 0.0f, 2.5f);
            obj.Scale *= 1.4f;
            obj.Children[0].AddScript<VoltmeterScript>();

            return obj;
        }
    }
}

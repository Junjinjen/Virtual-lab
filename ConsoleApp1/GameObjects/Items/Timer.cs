using JUnity;
using JUnity.Utilities;
using SharpDX;

namespace Lab3.GameObjects.Items
{
    public class Timer : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/timer.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Timer"));
            obj.Position = new Vector3(8.5f, -4.0f, 4.0f);

            return obj;
        }
    }
}

using JUnity;
using JUnity.Components.Physics;
using JUnity.Components.Physics.Colliders;
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
            var rigidbody = obj.Children[0].AddComponent<Rigidbody>();
            rigidbody.AddCollider(new BoxCollider("PlayButton", -Vector3.One, Vector3.One));

            return obj;
        }
    }
}

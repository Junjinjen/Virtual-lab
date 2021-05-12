using JUnity;
using JUnity.Components.Physics;
using JUnity.Components.Physics.Colliders;
using JUnity.Utilities;
using Lab3.Scripts.Interactions;
using SharpDX;

namespace Lab3.GameObjects.Items
{
    public class Metal : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/metal.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Metal"));
            obj.Position = new Vector3(6.5f, -4.0f, 0.0f);
            obj.Scale *= 0.75f;
            var rb = obj.AddComponent<Rigidbody>();
            rb.AddCollider(new BoxCollider(-Vector3.One, Vector3.One));
            obj.AddScript<MetalScript>();

            return obj;
        }
    }
}

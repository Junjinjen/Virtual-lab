using JUnity;
using JUnity.Components.Physics;
using JUnity.Components.Physics.Colliders;
using JUnity.Utilities;
using Lab3.Scripts.Interactions;
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
            obj.AddScript<TimerScript>();
            var rb = obj.Children[0].AddComponent<Rigidbody>();
            rb.AddCollider(new BoxCollider(new Vector3(-0.5f, -0.25f, -0.25f), new Vector3(0.5f, 0.25f, 0.25f)));
            rb = obj.Children[1].AddComponent<Rigidbody>();
            rb.AddCollider(new BoxCollider(new Vector3(-0.5f, -0.25f, -0.25f), new Vector3(0.5f, 0.25f, 0.25f)));
            rb = obj.Children[2].AddComponent<Rigidbody>();
            rb.AddCollider(new BoxCollider(new Vector3(-0.5f, -0.25f, -0.25f), new Vector3(0.5f, 0.25f, 0.25f)));

            return obj;
        }
    }
}

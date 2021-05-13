using JUnity;
using JUnity.Components.Physics;
using JUnity.Components.Physics.Colliders;
using JUnity.Utilities;
using Lab3.Scripts.Interactions;
using SharpDX;

namespace Lab3.GameObjects.Items
{
    public class Flask : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/flask.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Flask"));
            obj.Position = new Vector3(0.0f, -4.0f, 0.0f);
            obj.Scale *= 1.5f;
            var rb = obj.AddComponent<Rigidbody>();
            rb.AddCollider(new BoxCollider(new Vector3(-0.7f, 0f, -0.7f), new Vector3(0.7f, 1.3f, 0.7f)));
            obj.AddScript<FlaskScript>();

            return obj;
        }
    }
}

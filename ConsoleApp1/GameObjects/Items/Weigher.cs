using JUnity;
using JUnity.Components.Audio;
using JUnity.Components.Physics;
using JUnity.Components.Physics.Colliders;
using JUnity.Utilities;
using Lab3.Scripts.Interactions;
using SharpDX;

namespace Lab3.GameObjects.Items
{
    public class Weigher : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/weigher.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Weigher"));
            obj.Position = new Vector3(3.5f, -4.1f, 0.0f);
            obj.Scale *= 0.5f;
            obj.AddScript<WeigherScript>();
            var rb = obj.Children[0].AddComponent<Rigidbody>();
            rb.AddCollider(new BoxCollider(new Vector3(-2.2f, -0.25f, -2.2f), new Vector3(2.2f, 0.25f, 2.2f)));
            file = @"Audio/weigher.wav";
            var audio = obj.AddComponent<AudioPlayer>();
            audio.SetAudio(file);
            audio.Repeat = false;
            audio.Volume = 2;

            return obj;
        }
    }
}

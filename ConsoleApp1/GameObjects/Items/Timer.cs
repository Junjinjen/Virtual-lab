using JUnity;
using JUnity.Components.Audio;
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
            obj.Position = new Vector3(8.5f, -4f, 5.1f);
            obj.AddScript<TimerScript>();
            var rb = obj.Children[0].AddComponent<Rigidbody>();
            rb.AddCollider(new BoxCollider(new Vector3(-0.5f, -0.25f, -0.25f), new Vector3(0.5f, 0.25f, 0.25f)));
            rb = obj.Children[1].AddComponent<Rigidbody>();
            rb.AddCollider(new BoxCollider(new Vector3(-0.5f, -0.25f, -0.25f), new Vector3(0.5f, 0.25f, 0.25f)));
            rb = obj.Children[2].AddComponent<Rigidbody>();
            rb.AddCollider(new BoxCollider(new Vector3(-0.5f, -0.25f, -0.25f), new Vector3(0.5f, 0.25f, 0.25f)));
            file = @"Audio/timer.wav";
            var audio = obj.AddComponent<AudioPlayer>();
            audio.SetAudio(file);
            var auidoObject = new GameObject();
            file = @"Audio/button.wav";
            audio = auidoObject.AddComponent<AudioPlayer>();
            audio.SetAudio(file);
            audio.Repeat = false;
            obj.Children.Add(auidoObject);

            return obj;
        }
    }
}

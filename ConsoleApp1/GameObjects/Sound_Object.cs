using JUnity;
using JUnity.Components.Audio;
using JUnity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.GameObjects
{
    public class Sound_Object : IGameObjectCreator
    {
        private string nameObject;
        private string file;

        public Sound_Object(string nameObject, string file)
        {
            this.nameObject = nameObject;
            this.file = file;
        }

        public GameObject Create()
        {
            GameObject gameObject = new GameObject(nameObject);
            var audioPlayer = gameObject.AddComponent<AudioPlayer>();
            audioPlayer.SetAudio(file);
            audioPlayer.Repeat = false;

            return gameObject;
        }
    }
}

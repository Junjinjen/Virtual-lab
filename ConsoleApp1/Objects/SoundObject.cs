using JUnity;
using JUnity.Components.Audio;
using JUnity.Utilities;


namespace App.Objects
{
    public class SoundObject : IGameObjectCreator
    {
        private string _file;
        private string _objName;

        public SoundObject(string file, string objName)
        {
            _file = file;
            _objName = objName;
        }

        public GameObject Create()
        {
            GameObject gameObject = new GameObject(_objName);
            var audioPlayer = gameObject.AddComponent<AudioPlayer>();
            audioPlayer.SetAudio(_file);
            audioPlayer.Repeat = false;
            return gameObject;
        }
    }
}

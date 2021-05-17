using JUnity.Components.Interfaces;
using System.IO;
using SharpDX.XAudio2;
using SharpDX.Multimedia;

namespace JUnity.Components.Audio
{
    public class AudioPlayer : GameComponent, IUniqueComponent
    {
        private static XAudio2 _xAudio2 = new XAudio2();
        private static MasteringVoice masteringVoice = new MasteringVoice(_xAudio2);

        private SourceVoice _sourceVoice;
        private SoundStream _soundStream;
        private AudioBuffer _audioBuffersRing;

        public AudioPlayer(GameObject owner)
            : base(owner)
        {
            Repeat = true;
        }

        public bool Repeat { get; set; }

        public bool Finished { get; set; }

        public float Volume
        {
            get
            {
                _sourceVoice.GetVolume(out float value);
                return value;
            }
            set => _sourceVoice.SetVolume(value);
        }

        public void SetAudio(string fileName)
        {
            Dispose();

            _soundStream = new SoundStream(File.OpenRead(fileName));
            _sourceVoice = new SourceVoice(_xAudio2, _soundStream.Format);

            _audioBuffersRing = new AudioBuffer()
            {
                Stream = _soundStream.ToDataStream(),
                AudioBytes = (int)_soundStream.Length,
                Flags = BufferFlags.EndOfStream
            };

            _sourceVoice.SubmitSourceBuffer(_audioBuffersRing, _soundStream.DecodedPacketsInfo);

            _sourceVoice.BufferEnd += (context) =>
            {
                if (Repeat && !Finished)
                {
                    _sourceVoice.SubmitSourceBuffer(_audioBuffersRing, _soundStream.DecodedPacketsInfo);
                }
                else
                {
                    Finished = true;
                }
            };

            Finished = false;
        }

        public void Play()
        {
            if (Finished)
            {
                Finished = false;
                _sourceVoice.SubmitSourceBuffer(_audioBuffersRing, _soundStream.DecodedPacketsInfo);
            }

            _sourceVoice.Start();
        }

        public void Pause()
        {
            _sourceVoice.Stop();
        }

        public void Stop()
        {
            _sourceVoice.Stop();
            _sourceVoice.FlushSourceBuffers();
            Finished = true;
        }

        public override void Dispose()
        {
            _soundStream?.Close();
            _soundStream?.Dispose();

            _audioBuffersRing?.Stream.Close();
            _audioBuffersRing?.Stream.Dispose();
            
            _sourceVoice?.DestroyVoice();
            _sourceVoice?.Dispose();
        }
    }
}

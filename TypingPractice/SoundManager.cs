using System;
using System.IO;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace TypingPractice
{
    public class SoundManager : IDisposable
    {
        private const float VolumeGain     = 2.2f;
        private const float SpaceVolumeGain = 0.9f;
        private const int MixerSampleRate  = 44100;

        private static readonly string SoundDir =
            Path.Combine(Application.StartupPath, "sound");

        private readonly WaveOutEvent output;
        private readonly MixingSampleProvider mixer;
        private readonly byte[] typeData;
        private readonly byte[] spaceData;
        private readonly byte[] errorData;

        public bool Muted { get; set; }

        public SoundManager()
        {
            typeData  = LoadFile("type.wav");
            spaceData = LoadFile("space.mp3");
            errorData = LoadFile("error.wav");

            mixer = new MixingSampleProvider(
                WaveFormat.CreateIeeeFloatWaveFormat(MixerSampleRate, 2))
            {
                ReadFully = true
            };
            output = new WaveOutEvent { DesiredLatency = 50 };
            output.Init(mixer);
            output.Play();
        }

        public void PlayType()  => PlayWav(typeData);
        public void PlaySpace() => PlayMp3(spaceData);
        public void PlayError() => PlayWav(errorData);
        public void PlayEnter() => PlayWav(typeData);

        private static byte[] LoadFile(string filename)
        {
            string path = Path.Combine(SoundDir, filename);
            return File.Exists(path) ? File.ReadAllBytes(path) : null;
        }

        private void PlayWav(byte[] data)
        {
            if (Muted || data == null) return;
            try
            {
                var reader = new WaveFileReader(new MemoryStream(data));
                AddToMixer(reader);
            }
            catch { }
        }

        private void PlayMp3(byte[] data)
        {
            if (Muted || data == null) return;
            try
            {
                var reader = new Mp3FileReader(new MemoryStream(data));
                AddToMixer(reader, SpaceVolumeGain);
            }
            catch { }
        }

        private void AddToMixer(WaveStream reader, float volume = VolumeGain)
        {
            ISampleProvider sample = reader.ToSampleProvider();
            if (reader.WaveFormat.Channels == 1)
                sample = new MonoToStereoSampleProvider(sample);
            if (reader.WaveFormat.SampleRate != MixerSampleRate)
                sample = new WdlResamplingSampleProvider(sample, MixerSampleRate);
            sample = new VolumeSampleProvider(sample) { Volume = volume };
            mixer.AddMixerInput(sample);
        }

        public void Dispose()
        {
            output?.Stop();
            output?.Dispose();
        }
    }
}

using UnityEngine;

namespace GoldenAudio {
    [RequireComponent(typeof(AudioSource))]
    public class SineSynth : MonoBehaviour {
        public double Freq = 40d;
        public double Amp = 0.5d;

        private double sampleDuration;
        private double sampleRate;

        private SineUnitGen sineUnitGen;

        private void Awake() {
            sampleRate = AudioSettings.outputSampleRate;
            sampleDuration = 1f / sampleRate;

            sineUnitGen = new SineUnitGen(sampleRate, Freq, Amp);
        }

        private void OnAudioFilterRead(float[] buffer, int channels) {

            for (int s = 0; s < buffer.Length; s += channels) {

                sineUnitGen.Freq = Freq;
                sineUnitGen.Amp = Amp;

                double synthesis = sineUnitGen.NextSample();

                // Add synth to both channels of buffer
                for (int c = 0; c < channels; c++) {
                    buffer[s + c] += (float)synthesis;
                }
            }
        }
    }
}


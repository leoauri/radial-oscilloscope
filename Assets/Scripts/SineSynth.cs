using System;
using UnityEngine;

namespace GoldenAudio {
    [RequireComponent(typeof(AudioSource))]
    public class SineSynth : MonoBehaviour {
        public double Freq = 40d;
        public double Amp = 0.5d;

        private double sampleDuration;
        private double sampleRate;
        private double phase;

        private void Awake() {
            sampleRate = AudioSettings.outputSampleRate;
            sampleDuration = 1f / sampleRate;
        }

        private void OnAudioFilterRead(float[] buffer, int channels) {

            for (int s = 0; s < buffer.Length; s += channels) {

                // Synthesise sine according to phase
                double synthesis = Math.Sin(phase);
                synthesis *= Amp;

                // Increment phase (in radians) based on sample rate
                phase += Math.PI * Freq / sampleRate;

                // Add synth to both channels of buffer
                for (int c = 0; c < channels; c++) {
                    buffer[s + c] += (float)synthesis;
                }
            }

        }
    }

}


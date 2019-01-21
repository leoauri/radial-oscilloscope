using System;
using UnityEngine;

namespace GoldenAudio {
    [RequireComponent(typeof(AudioSource))]
    public class SineSynth : MonoBehaviour {
        public double Freq = 40d;
        public double Amp = 0.5d;

        private double sampleDuration;
        private double sampleRate;

        private void Awake() {
            sampleRate = AudioSettings.outputSampleRate;
            sampleDuration = 1f / sampleRate;
            Debug.Log(sampleDuration);
        }

        private void OnAudioFilterRead(float[] buffer, int channels) {
            double dspTime = AudioSettings.dspTime;


            for (int s = 0; s < buffer.Length; s += channels) {

                double sampleTime = dspTime + s / channels * sampleDuration;
                double synthesis = Math.Sin(Freq * sampleTime);
                synthesis *= Amp;

                // Add synth to both channels of buffer
                for (int c = 0; c < channels; c++) {
                    buffer[s + c] += (float)synthesis;
                }
            }

        }
    }

}


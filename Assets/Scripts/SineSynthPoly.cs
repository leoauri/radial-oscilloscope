using UnityEngine;
using System.Collections.Generic;

namespace GoldenAudio {
    [RequireComponent(typeof(AudioSource))]
    public class SineSynthPoly : MonoBehaviour {
        private double sampleDuration;
        private double sampleRate;

        private double synthesis;

        private List<SineUnitGen> generatorTable = new List<SineUnitGen>();

        private void Awake() {
            sampleRate = AudioSettings.outputSampleRate;
            sampleDuration = 1f / sampleRate;
        }

        public void NewSine(double Freq, double Amp, double Attack = 0) {
            generatorTable.Add(new SineUnitGen(sampleRate, Freq, Amp, Attack));
        }

        public void ReleaseSinesWithFreq(double Freq, double Release = 0) {
            for (int i = generatorTable.Count - 1; i >= 0; i--) {
                if (generatorTable[i].Freq == Freq) {
                    generatorTable[i].Release = Release;
                    generatorTable[i].IsReleasing = true;
                }
            }
        }

        private void OnAudioFilterRead(float[] buffer, int channels) {

            for (int s = 0; s < buffer.Length; s += channels) {

                synthesis = 0;

                // Traverse generator table in reverse so we can remove instances on the way if they are finished
                for (int i = generatorTable.Count - 1; i >= 0; i--) {
                    // If isFinished .RemoveAt(i);
                    if (generatorTable[i].IsFinished == true) {
                        generatorTable.RemoveAt(i);
                    }
                    else {
                        synthesis += generatorTable[i].NextSample();
                    }
                }

                //sineUnitGen.Freq = Freq;
                //sineUnitGen.Amp = Amp;

                // Add synth to both (all) channels of buffer
                for (int c = 0; c < channels; c++) {
                    buffer[s + c] += (float)synthesis;
                }
            }
        }
    }
}


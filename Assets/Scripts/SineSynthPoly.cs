using UnityEngine;
using System.Collections.Generic;

namespace GoldenAudio {
    [RequireComponent(typeof(AudioSource))]
    public class SineSynthPoly : MonoBehaviour {
        private double sampleDuration;
        private double sampleRate;

        private double synthesis;

        private int voiceCount = 0;

        private List<SineUnitGen> generatorTable = new List<SineUnitGen>();

        private void Awake() {
            sampleRate = AudioSettings.outputSampleRate;
            sampleDuration = 1f / sampleRate;
        }

        public int NewSine(double Freq, double Amp, double Attack = 0) {
            generatorTable.Add(new SineUnitGen(voiceCount, sampleRate, Freq, Amp, Attack));
            // Return UGen Voice
            return voiceCount++;
        }

        public void SetFreq(int Voice, double Freq) {
            for (int i = generatorTable.Count - 1; i >= 0; i--) {
                if (generatorTable[i].Voice == Voice) {
                    generatorTable[i].Freq = Freq;
                }
            }
        }

        public void SetAmp(int Voice, double Amp) {
            for (int i = generatorTable.Count - 1; i >= 0; i--) {
                if (generatorTable[i].Voice == Voice) {
                    generatorTable[i].Amp = Amp;
                }
            }
        }


        private void ReleaseI(int i, double Release = 0) {
                    generatorTable[i].Release = Release;
                    generatorTable[i].IsReleasing = true;
                }

        public void ReleaseVoice(int Voice, double Release = 0) {
            for (int i = generatorTable.Count - 1; i >= 0; i--) {
                if (generatorTable[i].Voice == Voice) {
                    ReleaseI(i, Release);
                }
            }
        }

        public void ReleaseAll(double Release = 0) {
            for (int i = generatorTable.Count - 1; i >= 0; i--) {
                ReleaseI(i, Release);
            }
        }

        public void ReleaseSinesWithFreq(double Freq, double Release = 0) {
            for (int i = generatorTable.Count - 1; i >= 0; i--) {
                if (generatorTable[i].Freq == Freq) {
                    ReleaseI(i, Release);
                }
            }
        }

        private void OnAudioFilterRead(float[] buffer, int channels) {

            for (int s = 0; s < buffer.Length; s += channels) {

                synthesis = 0;

                // Traverse generator table in reverse so we can remove instances on the way if they are finished
                for (int i = generatorTable.Count - 1; i >= 0; i--) {
                    // Make sure the object is instantiated before we start calling it
                    if (generatorTable[i] != null) {
                        // Remove if finished
                        if (generatorTable[i].IsFinished == true) {
                            generatorTable.RemoveAt(i);
                        }
                        else {
                            synthesis += generatorTable[i].NextSample();
                        }
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

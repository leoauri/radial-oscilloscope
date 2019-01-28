using System;

namespace GoldenAudio {
    public class SineUnitGen {
        public int Voice;
        public double Freq;
        public double Amp;
        public double Attack;
        private double sustain;
        public double Release;
        public bool IsReleasing;
        public bool IsFinished;

        private double SampleRate;
        private double sampleDuration;
        private double phase;

        public SineUnitGen(int voice, double sampleRate, double freq, double amp) {
            Voice = voice;
            Freq = freq;
            Amp = amp;
            sustain = amp;

            SampleRate = sampleRate;
            sampleDuration = 1f / sampleRate;
        }

        public SineUnitGen(int voice, double sampleRate, double freq, double amp, double attack) {
            Voice = voice;
            Freq = freq;
            sustain = amp;
            Attack = attack;

            SampleRate = sampleRate;
            sampleDuration = 1f / sampleRate;
        }


        public float NextSample() {
            if (IsReleasing) {
                if (Release <= 0 | Amp <= 0) {
                    // Mark for removal and return 0
                    IsFinished = true;
                    return 0;
                }
                else {
                    // Decrement amplitude and release time
                    Amp -= Amp / (Release * SampleRate);
                    Release -= sampleDuration;
                }

            }
            else if (Amp < sustain) {
                if (Attack <= 0) {
                    Amp = sustain;
                }
                else {
                    Amp += (sustain - Amp) / (Attack * SampleRate);
                    Attack -= sampleDuration;
                }
            }

            // Calculate sample
            double nextSample = Math.Sin(phase);
            nextSample *= Amp;

            // Increment phase (in radians) according to Freq of UGen
            phase += Math.PI * Freq / SampleRate;

            return (float)nextSample;
        }
    }
}

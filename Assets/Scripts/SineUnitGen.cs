using System;

namespace GoldenAudio {
    public class SineUnitGen {
        public double Freq;
        public double Amp;
        public double Release;
        public bool IsReleasing;
        public bool IsFinished;

        private double SampleRate;

        private double sampleDuration;
        private double phase;

        public SineUnitGen(double sampleRate, double freq, double amp) {
            Freq = freq;
            Amp = amp;

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

            // Calculate sample
            double nextSample = Math.Sin(phase);
            nextSample *= Amp;

            // Increment phase (in radians) according to Freq of UGen
            phase += Math.PI * Freq / SampleRate;

            return (float)nextSample;
        }
    }
}

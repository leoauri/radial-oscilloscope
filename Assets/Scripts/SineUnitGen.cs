using System;

namespace GoldenAudio {
    public class SineUnitGen {
        public double Freq;
        public double Amp;
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
            // Calculate sample
            double nextSample = Math.Sin(phase);
            nextSample *= Amp;

            // Increment phase (in radians) according to Freq of UGen
            phase += Math.PI * Freq / SampleRate;

            return (float)nextSample;
        }
    }
}

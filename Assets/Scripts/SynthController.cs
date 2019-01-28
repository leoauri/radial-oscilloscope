using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldenAudio {
    public class SynthController : MonoBehaviour {
        public SineSynthPoly Synth;

        private int wobbleVoice = -1;
        private double wobbleFreq;
        private double wobbleLFOFreq;
        private double wobbleLFOAmount;
        private double wobbleDuration;
        private double wobbleStart;
        private bool wobblingNow;

        public bool TestMode;


        // Wobble effect
        public void Wobble(double Duration, float Intensity = 1) {
            // Only one wobble at a time
            if (wobblingNow) {
                return;
            }
            RandomiseWobble(Intensity);
            wobbleDuration = Duration;
            wobbleStart = Time.time;
            wobbleVoice = Synth.NewSine(wobbleFreq + wobbleLFOAmount * Math.Sin(wobbleLFOFreq * Time.time), 0.2, 1);
            wobblingNow = true;
        }

        private void RandomiseWobble(float Intensity) {
            wobbleFreq = UnityEngine.Random.value * 800 + 200;
            wobbleLFOFreq = UnityEngine.Random.value * Intensity * 24 + 1;
            wobbleLFOAmount = UnityEngine.Random.value * wobbleFreq * 0.75 + 100;
        }

        private void ManageWobbles() {
            if (wobblingNow) {
                // Check for end
                if (Time.time > wobbleStart + wobbleDuration) {
                    Synth.ReleaseVoice(wobbleVoice, 1);
                    wobblingNow = false;
                }
            }
            // LFO Modulate
            Synth.SetFreq(wobbleVoice, wobbleFreq + wobbleLFOAmount * Math.Sin(wobbleLFOFreq * Time.time));
        }


        // Get spookier with Sine wave
        public void RandomSinePad(int number) {
            for (int i = 0; i < number; i++) {
                Synth.NewSine(UnityEngine.Random.value * 800 + 200, 0.03, 2);
            }
        }

        public void ReleaseAll() {
            Synth.ReleaseAll(Release: 2.5);
        }




        // Update is called once per frame
        void Update() {
            ManageWobbles();

            if (TestMode) {
                if (Input.GetKeyDown(KeyCode.Q)) {
                  Synth.NewSine(400, 0.2, 1);
                }
                if (Input.GetKeyDown(KeyCode.W)) {
                  Synth.NewSine(500, 0.2, 4);
                }
                if (Input.GetKeyDown(KeyCode.E)) {
                  Synth.NewSine(600, 0.2, 10);
                }
                if (Input.GetKeyDown(KeyCode.R)) {
                  Synth.NewSine(700, 0.2, 10);
                }
                if (Input.GetKeyDown(KeyCode.T)) {
                  Synth.NewSine(800, 0.2, 10);
                }

                if (Input.GetKeyDown(KeyCode.A)) {
                  Synth.ReleaseSinesWithFreq(400, 1);
                }
                if (Input.GetKeyDown(KeyCode.S)) {
                  Synth.ReleaseSinesWithFreq(500, 1);
                }
                if (Input.GetKeyDown(KeyCode.D)) {
                  Synth.ReleaseSinesWithFreq(600, 1);
                }
                if (Input.GetKeyDown(KeyCode.F)) {
                  Synth.ReleaseSinesWithFreq(700, 2);
                }
                if (Input.GetKeyDown(KeyCode.G)) {
                  Synth.ReleaseSinesWithFreq(800, 2.5);
                }

                if (Input.GetKeyDown(KeyCode.Z)) {
                    RandomSinePad(3);
                }
                if (Input.GetKeyDown(KeyCode.H)) {
                    ReleaseAll();
                }

                if (Input.GetKeyDown(KeyCode.J)) {
                    Wobble(1, Intensity: 1);
                }
                if (Input.GetKeyDown(KeyCode.U)) {
                    Wobble(1, Intensity: 10);
                }
            }
        }
    }
}

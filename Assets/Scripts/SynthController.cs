﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldenAudio {
    public class SynthController : MonoBehaviour {
        public SineSynthPoly Synth;

        // Start is called before the first frame update
        void Start() {
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.Q)) {
                Synth.NewSine(400, 0.2);
            }
            if (Input.GetKeyDown(KeyCode.W)) {
                Synth.NewSine(500, 0.2);
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                Synth.NewSine(600, 0.2);
            }
        }
    }
}

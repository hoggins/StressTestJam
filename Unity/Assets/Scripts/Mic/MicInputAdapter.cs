using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class MicInputAdapter : MonoBehaviour
    {

        public static float FinalLevel;

        public float SmoothPerc = 0.1f;
        public float Fade = 0.01f;
        
        private const int Rate = 5;
        
        private void Update()
        {
            var level = MicInput2.MicLoudness;

            FinalLevel = (FinalLevel + level - Fade) * SmoothPerc;

            FinalLevel = Mathf.Clamp01(FinalLevel);
            
        }
    }
}
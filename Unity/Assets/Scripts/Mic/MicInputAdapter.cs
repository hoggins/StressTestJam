using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class MicInputAdapter : MonoBehaviour
    {

        public static float FinalLevel;

        public float DecVal = -0.1f;
        public float IncVal = 0.5f;
        

        private float _lastLevel;
        
        private void Update()
        {
            var rawlevel = MicInput2.MicLoudness;
            
            var level = _lastLevel > rawlevel ? (DecVal) : IncVal * rawlevel ;
            _lastLevel = level;
            
            FinalLevel = (FinalLevel + level ) ;

            if (FinalLevel < 0.3)
                FinalLevel = 0;
            else
            FinalLevel = Mathf.Clamp01(FinalLevel);
        }
    }
}
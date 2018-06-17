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

        public float MedianStill = 1;
        public float MedianActive = 0.7f;
        public float RaiseTimeSec = 0.5f;
        
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

            if (FinalLevel > 0.3f)
                MicInput2.Inctance.MedianPercent = Mathf.Lerp(MicInput2.Inctance.MedianPercent, MedianActive, Time.deltaTime / RaiseTimeSec);
            else
                MicInput2.Inctance.MedianPercent = Mathf.Lerp(MedianStill, MicInput2.Inctance.MedianPercent, Time.deltaTime / RaiseTimeSec);
            
            MicInput2.Inctance.MedianPercent = Mathf.Clamp(MicInput2.Inctance.MedianPercent, MedianActive, MedianStill);
        }
    }
}
using Gyro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MicLevelIndicator : MonoBehaviour
    {
        private Image _indic;
        private Image _gyro;

        private void OnEnable()
        {
            //indic = GameObject.Find("Cube");
            _indic = GameObject.Find("MicIndic").GetComponent<Image>();
            _gyro = GameObject.Find("GyroIndic").GetComponent<Image>();
        }

        private void Update()
        {
            var level = MicInputAdapter.FinalLevel;
            //_indic.transform.localScale = new Vector3(1,level,1);
            _indic.rectTransform.sizeDelta = new Vector2(100*level, 15);


            var gyro = GyroInputAdapter.FinalTilt;
            level = (gyro + 1) /2;
            _gyro.rectTransform.sizeDelta = new Vector2(100*level, 15);
        }
    }
}
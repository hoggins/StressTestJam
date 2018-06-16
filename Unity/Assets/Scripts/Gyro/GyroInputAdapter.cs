using UnityEngine;
using UnityEngine.UI;

namespace Gyro
{
    public class GyroInputAdapter : MonoBehaviour
    {
        private int _tick;
        private Text _debugText;
        private GameObject _stick;

        public float SensFactor = 1.3f;
        
        public static float FinalTilt;

        private void Awake()
        {
            Application.targetFrameRate = 30;
            Input.gyro.enabled = true;
            _stick = new GameObject("GyroStic");
            _stick.transform.position = new Vector3(-1000,-1000,-1000);

        }

        private void Update()
        {
            Vector3 gyroEuler = Input.gyro.attitude.eulerAngles;
            var eulers = new Vector3(-1.0f * gyroEuler.x, -1.0f * gyroEuler.y, gyroEuler.z);
            
            _stick.transform.eulerAngles = eulers;

            Vector3 upVec = _stick.transform.InverseTransformDirection( Vector3.forward);

            FinalTilt = Mathf.Clamp(upVec.x * SensFactor, -1,1);

        }
    }
}
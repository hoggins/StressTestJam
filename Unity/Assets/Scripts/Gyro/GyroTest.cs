using UnityEngine;
using UnityEngine.UI;

namespace Gyro
{
    public class GyroTest : MonoBehaviour
    {
        private int _tick;
        private Text _debugText;
        private GameObject _stick;

        private void Awake()
        {
            Application.targetFrameRate = 30;
//            Input.gyro.enabled = true;
//            _debugText = GameObject.Find("Text").GetComponent<Text>();
//            _stick = GameObject.Find("Stick");

        }

        private void Update()
        {
         return;   
            Vector3 gyroEuler = Input.gyro.attitude.eulerAngles;
            _stick.transform.eulerAngles = new Vector3(-1.0f * gyroEuler.x, -1.0f * gyroEuler.y, gyroEuler.z);

            Vector3 upVec = _stick.transform.InverseTransformDirection(-1f * Vector3.forward);

            var proj = Vector3.Project(upVec, Vector3.forward);
            
//            if (_tick++%45!=0)
//                return;
            var sb = "";
            sb += "proj " + upVec + "\n";
            sb += "proj " + proj + "\n";
            sb += " rotationRateUnbiased " + Input.gyro.rotationRateUnbiased + "\n";
            sb += " gravity " + Input.gyro.gravity + "\n";
            sb += " rotationRate " + Input.gyro.rotationRate + "\n";
            sb += " attitude " + Input.gyro.attitude + "\n";
            _debugText.text = sb;
        }
    }
}
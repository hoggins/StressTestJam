using UnityEngine;

namespace DefaultNamespace
{
    public class GameFinishCamera : MonoBehaviour
    {
        public float AnimationTime = 2;
        private GameObject _startPos;
        private GameObject _endPos;


        public Camera Native;

        private void Awake()
        {
            _startPos = GameObject.Find("StartPos");
            _endPos = GameObject.Find("EndPos");
            
            Native = GetComponent<Camera>();
            Native.transform.position = _startPos.transform.position;
            Native.transform.rotation = _startPos.transform.rotation;
        }

        private void Update()
        {
            Native.transform.position = Vector3.Lerp(Native.transform.position, _endPos.transform.position, Time.deltaTime/AnimationTime);
            Native.transform.rotation = Quaternion.Lerp(Native.transform.rotation, _endPos.transform.rotation, Time.deltaTime/AnimationTime);
            
        }
    }
}
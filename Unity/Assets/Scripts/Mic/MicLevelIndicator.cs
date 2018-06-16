using UnityEngine;

namespace DefaultNamespace
{
    public class MicLevelIndicator : MonoBehaviour
    {
        private GameObject _indic;

        private void OnEnable()
        {
            _indic = GameObject.Find("Cube");
        }

        private void Update()
        {
            var level = MicInputAdapter.FinalLevel;
            _indic.transform.localScale = new Vector3(1,level,1);
        }
    }
}
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerHybridInput : MonoBehaviour
    {
        private PlayerControl _pc;
        private MicInputAdapter _micInput;

        private void Awake()
        {
            var obj = FindObjectOfType<MicInput2>();
            if (!obj)
                gameObject.AddComponent<MicInput2>();
            _micInput = FindObjectOfType<MicInputAdapter>();
            if (!_micInput)
                _micInput = gameObject.AddComponent<MicInputAdapter>();
            _pc = GetComponent<PlayerControl>();
        }

        void Update()
        {
            var input = new Vector2();


            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                input.x = -1;
            }

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                input.x = 1;
            }

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                input.y = 1;
            }

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                input.y = -1;
            }

            input.y = MicInputAdapter.FinalLevel;

            _pc.SetInput(input);
        }
    }
}
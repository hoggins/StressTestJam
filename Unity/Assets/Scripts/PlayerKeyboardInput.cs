using DefaultNamespace;
using Gyro;
using UnityEngine;

public class PlayerKeyboardInput : MonoBehaviour
{
  private PlayerControl _pc;
  private MicInputAdapter _micInput;


  void Awake()
  {
    _pc = GetComponent<PlayerControl>();
    var obj = FindObjectOfType<MicInput2>();
    if (!obj)
      gameObject.AddComponent<MicInput2>();
    _micInput = FindObjectOfType<MicInputAdapter>();
    if (!_micInput)
      _micInput = gameObject.AddComponent<MicInputAdapter>();
  }

  void Update()
  {
    var input = new Vector2();


    var isKeySide = false;
    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
    {
      input.x = -1;
      isKeySide = true;
    }

    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
    {
      input.x = 1;
      isKeySide = true;
    }

    if (!isKeySide)
    {
      input.x = GyroInputAdapter.FinalTilt;
    }

    var isKeyUp = false;
    if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
    {
      input.y = 1;
      isKeyUp = true;
    }

    if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
    {
      input.y = -1;
      isKeyUp = true;
    }
    
    if (!isKeyUp)
      input.y = MicInputAdapter.FinalLevel;

    _pc.SetInput(input);
  }
}

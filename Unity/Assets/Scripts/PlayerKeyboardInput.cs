using UnityEngine;

public class PlayerKeyboardInput : MonoBehaviour
{
  private PlayerControl _pc;


  void Awake()
  {
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

    _pc.SetInput(input);
  }
}

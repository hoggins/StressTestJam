using UnityEngine;

public class PlayerControl : MonoBehaviour
{
  public float ForwardMaxSpeed = 10;
  public float ForwardAcceleration = 1;

  public float DirectionChangeAcc = 5;
  public float DirectionMaxSpeed = 5f;

  public float SlowDown = 1f;

  public Vector3 Velocity;
  private Vector2 _input;

  void Start()
  {

  }

  public void SetInput(Vector2 input)
  {
    _input = input;
  }

  void Update()
  {
    Velocity += new Vector3(_input.x * DirectionChangeAcc, _input.y * DirectionChangeAcc, ForwardAcceleration)*Time.deltaTime;

    if (Mathf.Abs(_input.x) < 0.01f)
    {
      Velocity.x = Mathf.Lerp(Velocity.x, 0f, Time.deltaTime*SlowDown);
    }
    if (Mathf.Abs(_input.y) < 0.01f)
    {
      Velocity.y = Mathf.Lerp(Velocity.y, 0f, Time.deltaTime*SlowDown);
    }

    Velocity.z = Mathf.Min(ForwardMaxSpeed, Velocity.z);
    Velocity.y = Mathf.Sign(Velocity.y) * Mathf.Min(DirectionMaxSpeed, Mathf.Abs(Velocity.y));
    Velocity.z = Mathf.Sign(Velocity.z) * Mathf.Min(DirectionMaxSpeed, Mathf.Abs(Velocity.z));

    transform.position += Velocity*Time.deltaTime;
  }
}

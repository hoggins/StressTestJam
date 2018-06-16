using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
  public static PlayerControl I;

  public float ForwardMaxSpeedAccelerated = 10;
  public float ForwardMaxSpeed = 10;
  private float _currentMaxSpeed = 10;


  public float ForwardAcceleration = 1;

  public float DirectionChangeAcc = 5;
  public float DirectionChangeAccY = 5;
  public float DirectionMaxSpeed = 5f;

  public float SlowDown = 1f;
  public float FallSpeed = 0.25f;

  public Vector3 Velocity;

  public bool Active;

  public const int AkrobanchiksCount = 30;


  public GameObject AkrobanchikPrefab;
  private List<AkrobanchikController> _akrobanchiks;

  public Vector2 _input;

  public bool AllDead
  {
    get { return _akrobanchiks != null && _akrobanchiks.All(a => a == null || a.Dead); }
  }

  void Awake()
  {
    I = this;
  }

  void Start()
  {
    Velocity = new Vector3(0,0, ForwardMaxSpeedAccelerated);
  }

  public void SetInput(Vector2 input)
  {
    _input = input;

    if (_input.y < 0.01f)
    {
      _input.y = -FallSpeed;
    }
  }

  void Update()
  {
    if(!Active)
      return;

    UpdateControl();
    UpdateAkrobanchiks();
  }

  private void UpdateAkrobanchiks()
  {
    _akrobanchiks.RemoveAll((akrobanchik) => akrobanchik == null);

    for (int i = 0; i < _akrobanchiks.Count; i++)
    {
      var akrobanchik = _akrobanchiks[i];
      akrobanchik.SetIndex(i);
      akrobanchik.DoUpdate();
    }
  }

  private void UpdateControl()
  {
    _currentMaxSpeed = Mathf.Lerp(_currentMaxSpeed,
      _input.y > 0.01f ? ForwardMaxSpeedAccelerated : ForwardMaxSpeed,
      Time.deltaTime*(_input.y > 0.01f ? 3f : 0.5f));

    Velocity += new Vector3(_input.x*DirectionChangeAcc, _input.y*DirectionChangeAccY, ForwardAcceleration)*Time.deltaTime;

    if (Mathf.Abs(_input.x) < 0.01f)
    {
      Velocity.x = Mathf.Lerp(Velocity.x, 0f, Time.deltaTime*SlowDown);
    }
    if (Mathf.Abs(_input.y) < 0.01f)
    {
      Velocity.y = Mathf.Lerp(Velocity.y, 0f, Time.deltaTime*SlowDown);
    }

    Velocity.z = Mathf.Min(_currentMaxSpeed, Velocity.z);
    Velocity.y = Mathf.Sign(Velocity.y)*Mathf.Min(DirectionMaxSpeed, Mathf.Abs(Velocity.y));
    Velocity.x = Mathf.Sign(Velocity.x)*Mathf.Min(DirectionMaxSpeed, Mathf.Abs(Velocity.x));

    transform.position += Velocity*Time.deltaTime;
  }

  public void SetAkrobanchiks(List<AkrobanchikController> akrobanchiks)
  {
    _akrobanchiks = akrobanchiks;
    Active = true;

    GameController.I.StartSound.Play();
  }
}

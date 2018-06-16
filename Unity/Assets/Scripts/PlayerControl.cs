using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
  public static PlayerControl I;

  public float ForwardMaxSpeed = 10;
  public float ForwardAcceleration = 1;

  public float DirectionChangeAcc = 5;
  public float DirectionMaxSpeed = 5f;

  public float SlowDown = 1f;
  public float FallSpeed = 0.25f;

  public Vector3 Velocity;

  public const int AkrobanchiksCount = 30;


  public GameObject AkrobanchikPrefab;
  private List<AkrobanchikController> _akrobanchiks;

  private Vector2 _input;

  public bool AllDead
  {
    get { return _akrobanchiks.All(a => a == null || a.Dead); }
  }

  void Awake()
  {
    I = this;
  }

  void Start()
  {
    SpawnAkrobanchiks();
  }

  private void SpawnAkrobanchiks()
  {
    _akrobanchiks = new List<AkrobanchikController>();
    for (int i = 0; i < AkrobanchiksCount; i++)
    {
      var go = Instantiate(AkrobanchikPrefab, transform.position, Quaternion.identity);
      var ac = go.GetComponent<AkrobanchikController>();
      ac.SetIndex(i);

      _akrobanchiks.Add(ac);
    }
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
    }
  }

  private void UpdateControl()
  {
    Velocity += new Vector3(_input.x*DirectionChangeAcc, _input.y*DirectionChangeAcc, ForwardAcceleration)*Time.deltaTime;

    if (Mathf.Abs(_input.x) < 0.01f)
    {
      Velocity.x = Mathf.Lerp(Velocity.x, 0f, Time.deltaTime*SlowDown);
    }
    if (Mathf.Abs(_input.y) < 0.01f)
    {
      Velocity.y = Mathf.Lerp(Velocity.y, 0f, Time.deltaTime*SlowDown);
    }

    Velocity.z = Mathf.Min(ForwardMaxSpeed, Velocity.z);
    Velocity.y = Mathf.Sign(Velocity.y)*Mathf.Min(DirectionMaxSpeed, Mathf.Abs(Velocity.y));
    Velocity.x = Mathf.Sign(Velocity.x)*Mathf.Min(DirectionMaxSpeed, Mathf.Abs(Velocity.x));

    transform.position += Velocity*Time.deltaTime;
  }
}

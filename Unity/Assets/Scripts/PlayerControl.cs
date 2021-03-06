﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
  public static PlayerControl I;

  public float ForwardMaxSpeedAccelerated = 10;
  public float ForwardMaxSpeed = 10;

  public float MaxSpeedChangeSpeedUp = 3;
  public float MaxSpeedChangeSpeedDown = 1f;


  private float _currentMaxSpeed = 10;


  public float ForwardAcceleration = 1;

  public float DirectionChangeAcc = 5;
  public float DirectionChangeAccY = 5;
  public float DirectionMaxSpeed = 5f;

  public float SlowDown = 1f;
  public float FallSpeed = 0.25f;

  public Vector3 Velocity;

  public bool Active;
  
  public float FeedbackCooldown = 3;
  public const int AkrobanchiksFakeCount = 180;
  public const int AkrobanchiksCount = 60;
  public const int AkrobanchiksCountLength = 30;

  public GameObject AkrobanchikPrefab;
  public List<AkrobanchikController> _akrobanchiks;

  public Vector2 _input;
  private float _lastFeedbackTime;

  public bool AllDead
  {
    get { return Active && _akrobanchiks != null && _akrobanchiks.All(a => a == null || a.Dead); }
  }
  
  public float AlivePercent { get {return _akrobanchiks.Count / (float)AkrobanchiksCount; }}

  void Awake()
  {
    I = this;
  }

  void Start()
  {
    Velocity = new Vector3(0,0, ForwardMaxSpeedAccelerated/4);
  }

  public void SetInput(Vector2 input)
  {
    _input = input;

    if (input.y > 0.9f)
      TryPlayFeedbackSound();
    
    if (_input.y < 0.01f)
    {
      _input.y = -FallSpeed;
    }
  }

  private void TryPlayFeedbackSound()
  {
  if (_lastFeedbackTime + FeedbackCooldown > Time.time)
    return;
    _lastFeedbackTime = Time.time;
    GameController.I.WooHooSound.Play();
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
      akrobanchik.ShouldUseIndex = true;
      akrobanchik.SetIndex(i);
      akrobanchik.DoUpdate();
    }
  }

  private void UpdateControl()
  {
    _currentMaxSpeed = Mathf.Lerp(_currentMaxSpeed,
      _input.y > 0.01f ? ForwardMaxSpeedAccelerated : ForwardMaxSpeed,
      Time.deltaTime*(_input.y > 0.01f ? MaxSpeedChangeSpeedUp : MaxSpeedChangeSpeedDown));

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

    GameController.I.Music.Play();
    GameController.I.StartSound.Play();
  }
}

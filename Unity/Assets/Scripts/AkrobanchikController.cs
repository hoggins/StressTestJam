using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class AkrobanchikController : MonoBehaviour
{
  public float FollowSpeed;
  public float ZFollowSpeed;

  private int _index;
  public float XOffset = 2;
  public float YOffset = 2f;
  public float ZOffset = 0.1f;

  [NonSerialized]
  public bool ShouldUseIndex = true;

  private Vector3 _positionOffset;
  private Rigidbody _rb;

  private Vector3 _targetRotation;

  public bool Dead { get; set; }
  public bool Active { get; set; }

  private float _elapsed;
  private float _elapsed2;
  private Vector3 _pos;
  private float _rotation;

  private float range;
  private float range2;
  void Start ()
  {
    range = Random.Range(0, 10f);
    range2 = Random.Range(-10, 10f);
    _rb = GetComponent<Rigidbody>();

    GetPositionOffset();
    StartCoroutine(PositionOffsetChanger());
    _elapsed = Random.Range(-Mathf.PI, Mathf.PI);
    _elapsed2 = Random.Range(-Mathf.PI, Mathf.PI);
    _rotation = Random.Range(-1f, 1f);
  }

  private void GetPositionOffset()
  {
    _positionOffset = new Vector3(Random.Range(-XOffset, XOffset),
      Random.Range(-YOffset, YOffset),
      Random.Range(-ZOffset, ZOffset));
  }

  public void DoUpdate ()
  {
    if(!Active || Dead || !PlayerControl.I.Active)
      return;

    range = Mathf.Lerp(range, 0f, Time.deltaTime*0.25f);
    range2 = Mathf.Lerp(range2, 0f, Time.deltaTime*1f);

    _elapsed += Time.deltaTime;
    _elapsed2 += Time.deltaTime;

    _positionOffset.y += Mathf.Sin(_elapsed)*Time.deltaTime*0.25f;
    _positionOffset.x += Mathf.Cos(_elapsed2)*Time.deltaTime*0.5f;


    var pos = transform.position;
    var resultPos = PlayerControl.I.transform.position + _positionOffset;

    float indexSpeed;

    if (ShouldUseIndex)
      indexSpeed = PlayerControl.AkrobanchiksCountLength - _index%PlayerControl.AkrobanchiksCountLength;
    else
      indexSpeed = range;

    // var indexSpeed = PlayerControl.AkrobanchiksCountLength - _index%PlayerControl.AkrobanchiksCountLength;


    pos.x = Mathf.Lerp(pos.x + range2/100f, resultPos.x, Time.deltaTime*indexSpeed*Mathf.Sqrt(indexSpeed)*FollowSpeed);
    pos.y = Mathf.Lerp(pos.y, resultPos.y, Time.deltaTime*indexSpeed*Mathf.Sqrt(indexSpeed)*FollowSpeed);
    pos.z = Mathf.Lerp(pos.z, resultPos.z, Time.deltaTime*indexSpeed*ZFollowSpeed);

    _pos = pos;
    transform.position = pos;

    _targetRotation.z += Mathf.Sin(_elapsed*3)*Time.deltaTime*25;
    _targetRotation.y += Time.deltaTime*_rotation * 20f;

    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_targetRotation), Time.deltaTime*5f);
  }

  public void SetIndex(int index)
  {
    _index = index;
  }

  void OnCollisionEnter(Collision collision)
  {
    if (GameController.I.IsCompleted)
      return;
    if (collision.gameObject.CompareTag("Wall"))
    {
      Dead = true;
      _rb.isKinematic = false;
      Destroy(this);

      GameController.I.SlapSound.Play();
    }
  }

  private IEnumerator PositionOffsetChanger()
  {
    while (true)
    {
      yield return new WaitForSeconds(Random.Range(1f, 2f));
    //  GetPositionOffset();
    }
  }
}

using System.Collections;
using UnityEngine;

public class AkrobanchikController : MonoBehaviour
{
  public float FollowSpeed;
  public float ZFollowSpeed;

  private int _index;
  public float XOffset = 2;
  public float YOffset = 2f;
  public float ZOffset = 0.1f;

  private Vector3 _positionOffset;
  private Rigidbody _rb;

  public bool Dead { get; set; }

  private float _elapsed;
  private float _elapsed2;
  private Vector3 _pos;

  void Start ()
  {
    _rb = GetComponent<Rigidbody>();

    GetPositionOffset();
    StartCoroutine(PositionOffsetChanger());
    _elapsed = Random.Range(-Mathf.PI, Mathf.PI);
    _elapsed2 = Random.Range(-Mathf.PI, Mathf.PI);
  }

  private void GetPositionOffset()
  {
    _positionOffset = new Vector3(Random.Range(-XOffset, XOffset),
      Random.Range(-YOffset, YOffset),
      Random.Range(-ZOffset, ZOffset));
  }

  public void DoUpdate ()
  {
    if(Dead || !PlayerControl.I.Active)
      return;

    _elapsed += Time.deltaTime;
    _elapsed2 += Time.deltaTime;

    _positionOffset.y += Mathf.Sin(_elapsed)*Time.deltaTime*0.25f;
    _positionOffset.x += Mathf.Cos(_elapsed2)*Time.deltaTime*0.5f;


    var pos = transform.position;
    var resultPos = PlayerControl.I.transform.position + _positionOffset;
    var indexSpeed = PlayerControl.AkrobanchiksCount - _index;

    pos.x = Mathf.Lerp(pos.x, resultPos.x, Time.deltaTime*indexSpeed*Mathf.Sqrt(indexSpeed)*FollowSpeed);
    pos.y = Mathf.Lerp(pos.y, resultPos.y, Time.deltaTime*indexSpeed*Mathf.Sqrt(indexSpeed)*FollowSpeed);
    pos.z = Mathf.Lerp(pos.z, resultPos.z, Time.deltaTime*indexSpeed*ZFollowSpeed);

    _pos = pos;
    transform.position = pos;


    transform.Rotate(0, 0, Mathf.Sin(_elapsed*3)*Time.deltaTime*25);

    //transform.position = Vector3.Lerp(transform.position,
    //  PlayerControl.I.transform.position + _positionOffset,
    //  Time.deltaTime*(PlayerControl.AkrobanchiksCount - _index)*FollowSpeed);
  }

  public void SetIndex(int index)
  {
    _index = index;
  }

  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("Wall"))
    {
      Dead = true;
      _rb.isKinematic = false;
      Destroy(this);
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

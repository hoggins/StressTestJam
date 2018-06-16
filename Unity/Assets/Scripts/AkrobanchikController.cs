using System.Collections;
using UnityEngine;

public class AkrobanchikController : MonoBehaviour
{
  public float FollowSpeed;
  private int _index;
  public float XOffset = 2;
  public float YOffset = 2f;
  public float ZOffset = 0.1f;

  public Vector3 _positionOffset;
  private float _elapsed;
  private float _elapsed2;

  void Start ()
  {
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

  void Update ()
  {
    _elapsed += Time.deltaTime;
    _elapsed2 += Time.deltaTime;

    _positionOffset.y += Mathf.Sin(_elapsed)*Time.deltaTime*0.25f;
    _positionOffset.x += Mathf.Cos(_elapsed2)*Time.deltaTime*0.5f;

	  transform.position = Vector3.Lerp(transform.position,
	    PlayerControl.I.transform.position + _positionOffset,
	    Time.deltaTime*Mathf.Sqrt(_index)*FollowSpeed);
	}

  public void SetIndex(int index)
  {
    _index = index;
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

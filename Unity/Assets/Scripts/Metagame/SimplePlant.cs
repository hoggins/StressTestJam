using UnityEngine;

public class SimplePlant : MonoBehaviour
{
  public AnimationCurve Curve;
  public Vector3 TargetScale;
  public float Duration;

  private float _elapsed;
  public Vector3 InitialScale = new Vector3(0,0,0);

	void Awake ()
	{
	  transform.localScale = InitialScale;
	}
	
	void Update ()
	{
	  _elapsed += Time.deltaTime;
	  if (_elapsed/Duration > 1f)
	  {
	    transform.localScale = Vector3.Lerp(InitialScale, TargetScale, Curve.Evaluate(1f));
      Destroy(this);
	  }
    else
	    transform.localScale = Vector3.Lerp(InitialScale, TargetScale, Curve.Evaluate(_elapsed/Duration));
	}
}


using UnityEngine;

public class CameraController : MonoBehaviour
{

  public GameObject Target;
  public float Speed;
  public float SpeedOnStart = 1;

  private float _currentSpeed;

  public Vector3 PositionOnStart;
  public Vector3 Offset;
  public Vector2 OffsetYClamp;
  public Vector2 OffsetXClamp;
  public static CameraController I;
  public Camera Native;

  void Awake()
  {
    I = this;
    Native = GetComponent<Camera>();
    _currentSpeed = SpeedOnStart;
  }

  void Start ()
  {
		
	}
	
	void LateUpdate ()
	{
	  if (!PlayerControl.I.Active)
	  {
	    transform.position = PositionOnStart;
      return;
	  }

	  if(Target == null)
      return;

	  _currentSpeed = Mathf.Lerp(_currentSpeed, Speed, Time.deltaTime);

		var lerpZ = Mathf.Lerp(transform.position.z, Target.transform.position.z + Offset.z, Time.deltaTime*_currentSpeed);
		var lerpY = Mathf.Lerp(transform.position.y, Target.transform.position.y /*+ Offset.y*/, Time.deltaTime*(_currentSpeed*0.1f));
		var lerpX = Mathf.Lerp(transform.position.x, Target.transform.position.x /*+ Offset.x*/, Time.deltaTime*(_currentSpeed*0.1f));
		lerpY = Mathf.Clamp(lerpY, OffsetYClamp.x, OffsetYClamp.y);
		lerpX = Mathf.Clamp(lerpX, OffsetXClamp.x, OffsetXClamp.y);
		transform.position = new Vector3(lerpX, lerpY , lerpZ);

	  //transform.position = Vector3.Lerp(transform.position, Target.transform.position + Offset, Time.deltaTime);
	}
}

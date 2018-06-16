using UnityEngine;

public class CameraController : MonoBehaviour
{

  public GameObject Target;
  public float Speed;
  public Vector3 Offset;
  public Vector2 OffsetYClamp;
  public static CameraController I;
  public Camera Native;

  void Awake()
  {
    I = this;
    Native = GetComponent<Camera>();
  }

  void Start ()
  {
		
	}
	
	void LateUpdate ()
	{
    if(Target == null)
      return;

		var lerpZ = Mathf.Lerp(transform.position.z, Target.transform.position.z + Offset.z, Time.deltaTime*Speed);
		var lerpY = Mathf.Lerp(transform.position.y, Target.transform.position.y /*+ Offset.y*/, Time.deltaTime*(Speed*0.3f));
		lerpY = Mathf.Clamp(lerpY, OffsetYClamp.x, OffsetYClamp.y);
		transform.position = new Vector3(Offset.x, lerpY , lerpZ);

	  //transform.position = Vector3.Lerp(transform.position, Target.transform.position + Offset, Time.deltaTime);
	}
}

using UnityEngine;

public class CameraController : MonoBehaviour
{

  public GameObject Target;
  public float Speed;
  public Vector3 Offset;
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

    transform.position = new Vector3(Offset.x, Offset.y , Mathf.Lerp(transform.position.z, Target.transform.position.z + Offset.z, Time.deltaTime*Speed));

	  //transform.position = Vector3.Lerp(transform.position, Target.transform.position + Offset, Time.deltaTime);
	}
}

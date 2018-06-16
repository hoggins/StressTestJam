using UnityEngine;

public class RotateAround : MonoBehaviour {

	void Start () {
		
	}
	

	void Update ()
  {
		transform.Rotate(0, 0, Time.deltaTime*GameController.I.RotationSpeed);
	}
}

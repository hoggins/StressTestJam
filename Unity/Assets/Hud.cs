using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
  public Text HP;

	void Start ()
  {
		
	}
	
	void Update ()
	{
	  if (!PlayerControl.I.Active)
	  {
	    HP.text = "100%";
	  }
	  else
	  {
	    var value =Mathf.RoundToInt((float) PlayerControl.I._akrobanchiks.Count/(float)PlayerControl.AkrobanchiksCount*100f);
	    HP.text = value.ToString() + "%";
	  }
	}
}

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
	    HP.text = ((PlayerControl.AkrobanchiksCount/PlayerControl.AkrobanchiksCount)*100).ToString("D") + "%";
	  }
	  else
	  {
	    HP.text = ((PlayerControl.I._akrobanchiks.Count/PlayerControl.AkrobanchiksCount)*100).ToString("D") + "%";
	  }
	}
}

using System.Collections.Generic;
using UnityEngine;

public class StartObject : MonoBehaviour {

  public GameObject AkrobanchikPrefab;
  public List<GameObject> SpawnPoints;
  private List<AkrobanchikController> _akrobanchiks;

  private bool _didSet;

  void Start ()
	{
	  SpawnAkrobanchiks();
	}
	
	void Update ()
  {
    if (_didSet)
      return;

	  if (PlayerControl.I._input.y > 0)
	  {
      _didSet = true;
      PlayerControl.I.SetAkrobanchiks(_akrobanchiks);
	  }
  }

  private void SpawnAkrobanchiks()
  {
    _akrobanchiks = new List<AkrobanchikController>();
    for (int i = 0; i < PlayerControl.AkrobanchiksCount; i++)
    {
	    var spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Count)];
	    var pointPos = spawnPoint.transform.position;

	    
	    RaycastHit hit = new RaycastHit();
	    for (int j = 0; j < 1000; j++)
	    {
		    Vector3 spawnPosition = Random.onUnitSphere * 3 + pointPos;
		    var spawnDir = pointPos - spawnPosition;

		    var dot = Vector3.Dot(spawnDir, Vector3.up);
		    if (dot > 0.2f)
			    continue;

		    var isHit = Physics.Raycast(spawnPosition, spawnDir, out hit, 10, LayerMask.GetMask("StartPoint"));
		    if (!isHit)
		    {
			    continue;
		    }
		    break;
	    }

	    Quaternion spawnRotation = Quaternion.LookRotation(-hit.normal) * Quaternion.Euler(-90,0,0);
      var go = Instantiate(AkrobanchikPrefab,  hit.point+hit.normal*0.6f, spawnRotation);
      var ac = go.GetComponent<AkrobanchikController>();
      ac.SetIndex(i);

      _akrobanchiks.Add(ac);
    }

  }
}

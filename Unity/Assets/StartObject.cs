using System.Collections.Generic;
using UnityEngine;

public class StartObject : MonoBehaviour {

  public GameObject AkrobanchikPrefab;
  public List<GameObject> SpawnPoints;
  private List<AkrobanchikController> _akrobanchiks;

  void Start ()
	{
	  SpawnAkrobanchiks();
	}
	
	void Update ()
  {
	  if (PlayerControl.I._input.y > 0)
	  {
      PlayerControl.I.SetAkrobanchiks(_akrobanchiks);
	  }
  }

  private void SpawnAkrobanchiks()
  {
    _akrobanchiks = new List<AkrobanchikController>();
    for (int i = 0; i < PlayerControl.AkrobanchiksCount; i++)
    {
      var spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Count)];

      var go = Instantiate(AkrobanchikPrefab, spawnPoint.transform.position, Quaternion.identity);
      var ac = go.GetComponent<AkrobanchikController>();
      ac.SetIndex(i);

      _akrobanchiks.Add(ac);
    }

  }
}

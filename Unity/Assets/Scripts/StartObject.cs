using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartObject : MonoBehaviour {

  public GameObject AkrobanchikPrefab;
  public List<GameObject> SpawnPoints;
  private List<AkrobanchikController> _akrobanchiks;
  private List<AkrobanchikController> _fakeAkrobanchiks;

  private bool _didSet;
  private float _elapsed = Mathf.PI/2f;

  void Start ()
	{
	  SpawnAkrobanchiks();
	}
	
	void Update ()
  {
	  if (!_didSet)
	  {
	    if (PlayerControl.I._input.y > 0.5f)
	    {
	      _didSet = true;
	      PlayerControl.I.SetAkrobanchiks(_akrobanchiks);

	      foreach (var a in _akrobanchiks)
	      {
	        a.transform.SetParent(null);
	      }

        foreach (var a in _fakeAkrobanchiks)
	      {
	        a.transform.SetParent(null);
	      }

	      StartCoroutine(KillFake());
	    }
	  }

	  foreach (var fake in _fakeAkrobanchiks)
	  {
	    fake.DoUpdate();
	  }

	  _elapsed += Time.deltaTime;
    transform.Rotate(0, 0, Mathf.Sin(_elapsed)/10);
  }

  private IEnumerator KillFake()
  {
    int i = 0;
    foreach (var fake in _fakeAkrobanchiks)
    {
      fake.Active = true;
      i++;
      if(i % 5 == 0)
        yield return null;
    }

    yield return new WaitForSeconds(3.5f);

    foreach (var fake in _fakeAkrobanchiks)
    {
      fake.Dead = true;
      yield return null;
    }
  }

  private void SpawnAkrobanchiks()
  {
    _akrobanchiks = new List<AkrobanchikController>();
    _fakeAkrobanchiks = new List<AkrobanchikController>();
    for (int i = 0; i < PlayerControl.AkrobanchiksCount; i++)
    {
	    var ac = SpawnAkrobanchik(i);
      ac.ShouldUseIndex = true;
      ac.Active = true;
      _akrobanchiks.Add(ac);
    }

    for (int i = 0; i < PlayerControl.AkrobanchiksFakeCount; i++)
    {
      var ac = SpawnAkrobanchik(i + PlayerControl.AkrobanchiksCount);
      ac.ShouldUseIndex = false;
      ac.Active = false;
      Destroy(ac.GetComponent<Rigidbody>());

      foreach (var c in ac.GetComponentsInChildren<Collider>())
      {
        Destroy(c);
      }

      _fakeAkrobanchiks.Add(ac);
    }
  }

  private AkrobanchikController SpawnAkrobanchik(int i)
  {
    var spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Count)];
    var pointPos = spawnPoint.transform.position;


    RaycastHit hit = new RaycastHit();
    for (int j = 0; j < 1000; j++)
    {
      Vector3 spawnPosition = Random.onUnitSphere*3 + pointPos;
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

    Quaternion spawnRotation = Quaternion.LookRotation(-hit.normal)*Quaternion.Euler(-90, 0, 0);
    var go = Instantiate(AkrobanchikPrefab, hit.point + hit.normal*0.6f, spawnRotation);
    var ac = go.GetComponent<AkrobanchikController>();
    ac.SetIndex(i);
    ac.transform.SetParent(transform);

    return ac;
  }
}

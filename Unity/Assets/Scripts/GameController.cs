using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  public static GameController I;

  public GameObject WallPrefab;
  public GameObject AllDeadPrefab;
  public float WallLength = 50;
  public float RotationSpeed = 8f;

  public GameObject RotationRoot;


  private GameObject _allDead;

  void Awake()
  {
    I = this;
  }

  void Start()
  {
    for (int i = 0; i < 100; i++)
    {
      var wall = Instantiate(WallPrefab, new Vector3(0, 10, i*WallLength), Quaternion.identity, RotationRoot.transform);
    }
  }

  void Update()
  {
    if (PlayerControl.I.AllDead && _allDead == null)
    {
      ShowAllDead();
    }
  }

  private void ShowAllDead()
  {
    _allDead = Instantiate(AllDeadPrefab);

    StartCoroutine(RestartCoroutine());
  }

  private IEnumerator RestartCoroutine()
  {
    yield return new WaitForSeconds(5.0f);

    SceneManager.LoadScene("gameplay");
  }
}

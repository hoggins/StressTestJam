using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  public static GameController I;

  public AudioSource StartSound;
  public AudioSource LoseSound;
  public AudioSource WinSound;
  public AudioSource SlapSound;
  public AudioSource Music;

  public GameObject AllDeadPrefab;
  public GameObject AllFinishedPrefab;

  public GameObject BlackOut;

  public float RotationSpeed = 8f;

  public bool IsCompleted { get; private set; }


  private GameObject _allDead;

  void Awake()
  {
    Application.targetFrameRate = 30;
    
    I = this;
  }

  void Start()
  {
    CreateBlackout(Color.black, false);
  }

  void Update()
  {
    if (PlayerControl.I.AllDead && _allDead == null)
    {
      LoseSound.Play();
      ShowAllDead();
    }
  }

  public void CreateBlackout(Color color, bool hide)
  {
    var b = Instantiate(BlackOut);
    var bo = b.GetComponent<BlackOut>();
    bo.SetColor(color, hide);
  }

  private void ShowAllDead()
  {
    _allDead = Instantiate(AllDeadPrefab);

    StartCoroutine(RestartCoroutine());
  }

  private IEnumerator RestartCoroutine()
  {
    yield return new WaitForSeconds(2.0f);

    SceneManager.LoadScene("gameplay");
  }

  public void CompleteGame()
  {
    if(IsCompleted)
      return;

    WinSound.Play();
    IsCompleted = true;
    DataModel.SetBattleScore(PlayerControl.I.AlivePercent);
    ShowGameFinished();
  }
  private void ShowGameFinished()
  {
    _allDead = Instantiate(AllFinishedPrefab);

    StartCoroutine(FinishedCoroutine());
  }

  private IEnumerator FinishedCoroutine()
  {
    //yield return new WaitForSeconds(0.5f);
    //CreateBlackout(Color.white, true);
    yield return new WaitForSeconds(0.5f);
    CreateBlackout(Color.black, true);
    
    yield return new WaitForSeconds(2.0f);

    SceneManager.LoadScene("gameFinished");
  }
}

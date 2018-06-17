using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MenuController : MonoBehaviour
    {
      public GameObject BlackOut;
        private PlanetMetaController _plants;

      public Sprite MuteSprite;
      public Sprite UnMuteSprite;
      private Image _muteButton;
      private GameObject _infoRoot;
      private CanvasGroup  _infoRootCanvas;

      private void Awake()
      {
        DataModel.Initialize();
        _plants = FindObjectOfType<PlanetMetaController>();
        _plants.SetScore(DataModel.LastBattleScore, true);

        var b = Instantiate(BlackOut);
        var bo = b.GetComponent<BlackOut>();
        bo.SetColor(Color.black, false);

        _infoRoot = GameObject.Find("InfoRoot");
        _infoRootCanvas = _infoRoot.GetComponent<CanvasGroup>();
        _infoRoot.SetActive(false);
        
        _muteButton = GameObject.Find("Mute").GetComponent<Image>();
        UpdateMute();
      }

      public void ToBattle()
      {
        StartCoroutine(BattleCoroutine());
      }

      public void DoMute()
      {
        AudioListener.pause = !AudioListener.pause;
        UpdateMute();
      }

      public void DoReset()
      {
        PlayerPrefs.DeleteAll();
        DataModel.Initialize();
        _plants.SetScore(DataModel.LastBattleScore, true);
      }

      private void UpdateMute()
      {
        if (AudioListener.pause)
          _muteButton.sprite = UnMuteSprite;
        else
          _muteButton.sprite = MuteSprite;
      }

      public void ToInfo()
      {
        StartCoroutine(Tween(_infoRoot.activeSelf));
      }

      private IEnumerator Tween(bool hide)
      {

        if (!hide)
          _infoRoot.SetActive(true);
        
        var elapsed = 0f;
        var duration = 0.3f;

        float val;
        do
        {
          elapsed += Time.deltaTime;
          val = Mathf.Lerp(!hide ? 0f : 1f, hide ? 0f : 1f, elapsed / duration);
          _infoRootCanvas.alpha = val;
          yield return null;
        } while (elapsed < duration);

        if (hide)
          _infoRoot.SetActive(false);
      }

      private IEnumerator BattleCoroutine()
      {
        var b = Instantiate(BlackOut);
        var bo = b.GetComponent<BlackOut>();
        bo.SetColor(Color.black, true);

        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("gameplay");
      }
    }
}
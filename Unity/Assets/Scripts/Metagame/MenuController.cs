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

      private void Awake()
      {
        DataModel.Initialize();
        _plants = FindObjectOfType<PlanetMetaController>();
        _plants.SetScore(DataModel.BestBattleScore, true);

        var b = Instantiate(BlackOut);
        var bo = b.GetComponent<BlackOut>();
        bo.SetColor(Color.black, false);

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

      private void UpdateMute()
      {
        if (AudioListener.pause)
          _muteButton.sprite = UnMuteSprite;
        else
          _muteButton.sprite = MuteSprite;
      }

      public void ToInfo()
      {
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
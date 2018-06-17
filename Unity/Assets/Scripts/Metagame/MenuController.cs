using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class MenuController : MonoBehaviour
    {
      public GameObject BlackOut;
        private PlanetMetaController _plants;

      private void Awake()
      {
        DataModel.Initialize();
        _plants = FindObjectOfType<PlanetMetaController>();
        _plants.SetScore(DataModel.BestBattleScore, true);

        var b = Instantiate(BlackOut);
        var bo = b.GetComponent<BlackOut>();
        bo.SetColor(Color.black, false);
      }

      public void ToBattle()
      {
        StartCoroutine(BattleCoroutine());
      }

      private IEnumerator BattleCoroutine()
      {
        var b = Instantiate(BlackOut);
        var bo = b.GetComponent<BlackOut>();
        bo.SetColor(Color.black, false);

        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("gameplay");
      }
    }
}
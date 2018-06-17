using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameFinishController : MonoBehaviour
    {
      public GameObject BlackOut;
        private PlanetMetaController _plants;
        private GameFinishUi _ui;

        private void Awake()
        {
            _plants = FindObjectOfType<PlanetMetaController>();
            _plants.SetScore(DataModel.LastBattleScore);
            _ui = GetComponent<GameFinishUi>();
            _ui.SetScore(DataModel.LastBattleScore);

                  var b = Instantiate(BlackOut);
        var bo = b.GetComponent<BlackOut>();
        bo.SetColor(Color.black, false);
        }

        public void ToMenu()
        {
            SceneManager.LoadScene("mainMenu");
        }
    }
}
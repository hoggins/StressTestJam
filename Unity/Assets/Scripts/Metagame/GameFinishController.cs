using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameFinishController : MonoBehaviour
    {
        private PlanetMetaController _plants;
        private GameFinishUi _ui;

        private void Awake()
        {
            _plants = FindObjectOfType<PlanetMetaController>();
            _plants.SetScore(DataModel.LastBattleScore);
            _ui = GetComponent<GameFinishUi>();
            _ui.SetScore(DataModel.LastBattleScore);
        }

        public void ToMenu()
        {
            SceneManager.LoadScene("mainMenu");
        }
    }
}
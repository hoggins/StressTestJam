using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class MenuController : MonoBehaviour
    {
        private PlanetMetaController _plants;

        private void Awake()
        {
            DataModel.Initialize();
            _plants = FindObjectOfType<PlanetMetaController>();
            _plants.SetScore(DataModel.LastBattleScore, true);
        }

        public void ToBattle()
        {
            SceneManager.LoadScene("gameplay");
        }
    }
}
using UnityEngine;

namespace DefaultNamespace
{
    public static class DataModel
    {
        public static int LastBattleScore = 78; //{ get; private set; }

        public static void Initialize()
        {
            LastBattleScore = PlayerPrefs.GetInt("data.lastScore");
        }
        
        public static void SetBattleScore(float score)
        {
            LastBattleScore = Mathf.CeilToInt(score*100);
            
            PlayerPrefs.SetInt("data.lastScore", LastBattleScore);
        }
    }
}
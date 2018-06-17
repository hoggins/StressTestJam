using System;
using UnityEngine;

namespace DefaultNamespace
{
    public static class DataModel
    {
        public static int LastBattleScore = 100; //{ get; private set; }
        public static int BestBattleScore = 78; //{ get; private set; }
        public static int UserLevel = 0; //{ get; private set; }

        public static void Initialize()
        {
            LastBattleScore = PlayerPrefs.GetInt("data.lastScore");
            BestBattleScore = PlayerPrefs.GetInt("data.bestScore");
        }
        
        public static void SetBattleScore(float score)
        {
            LastBattleScore = Mathf.CeilToInt(score*100);
            
            PlayerPrefs.SetInt("data.lastScore", LastBattleScore);

            BestBattleScore = Math.Max(BestBattleScore, LastBattleScore);
            PlayerPrefs.SetInt("data.bestScore", BestBattleScore);
        }
    }
}
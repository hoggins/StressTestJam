using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class MapBuilder : MonoBehaviour
    {
      [Serializable]
      public class Level
      {
        public GameObject[] LayoutPrefabs;
        public int ChanksCount = 8;

      }

      public GameObject WallPrefab;
        public GameObject CompleteTriggerPrefab;
      public Level[] Levels;
        public float WallLength = 50;

        public GameObject RotationRoot;
        

        private void Awake()
        {
        }

        void Start()
        {
          var level = Levels[Mathf.Clamp(DataModel.UserLevel, 0, Levels.Length - 1)];

            for (int i = 0; i < level.ChanksCount; i++)
            {
                var position = new Vector3(0, 10, i*WallLength);
                var tube = Instantiate(WallPrefab, position, Quaternion.identity, RotationRoot.transform);

              if (i > 1)
              {
                var layout = level.LayoutPrefabs[i%level.LayoutPrefabs.Length];
                var wall = Instantiate(layout, position, Quaternion.identity, RotationRoot.transform);
              }
            }
            var complete = Instantiate(CompleteTriggerPrefab, new Vector3(0, 10, level.ChanksCount*WallLength - WallLength/2), Quaternion.identity, RotationRoot.transform);
        }
    }
}
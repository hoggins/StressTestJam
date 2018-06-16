using UnityEngine;

namespace DefaultNamespace
{
    public class MapBuilder : MonoBehaviour
    {
        public int ChanksCount = 2;
        public GameObject WallPrefab;
        public GameObject CompleteTriggerPrefab;
        public GameObject[] LayoutPrefabs;
        public float WallLength = 50;

        public GameObject RotationRoot;
        

        private void Awake()
        {
        }

        void Start()
        {
            for (int i = 0; i < ChanksCount; i++)
            {
                var position = new Vector3(0, 10, i*WallLength);
                var tube = Instantiate(WallPrefab, position, Quaternion.identity, RotationRoot.transform);
                var layout = LayoutPrefabs[i % LayoutPrefabs.Length];
                var wall = Instantiate(layout, position, Quaternion.identity, RotationRoot.transform);
            }
            var complete = Instantiate(CompleteTriggerPrefab, new Vector3(0, 10, ChanksCount*WallLength - WallLength/2), Quaternion.identity, RotationRoot.transform);
        }
    }
}
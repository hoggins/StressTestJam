using UnityEngine;

namespace DefaultNamespace
{
    public class MapBuilder : MonoBehaviour
    {
        public GameObject WallPrefab;
        public GameObject[] LayoutPrefabs;
        public float WallLength = 50;

        public GameObject RotationRoot;

        private void Awake()
        {
        }

        void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                var position = new Vector3(0, 10, i*WallLength);
                var tube = Instantiate(WallPrefab, position, Quaternion.identity, RotationRoot.transform);
                var layout = LayoutPrefabs[i % LayoutPrefabs.Length];
                var wall = Instantiate(layout, position, Quaternion.identity, RotationRoot.transform);
            }
        }
    }
}
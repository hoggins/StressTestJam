using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlanetMetaController : MonoBehaviour
    {
        public int TargetPlants = 700; // this is used as 100% result
        public float SpawningTime = 1.5f;
        public float RotationSpeed = 5;
        public float SizeDiffMin = 0.75f;
        public float SizeDiffMax = 1.25f;
        
        public GameObject PlantPrefab;

        private int _actualPlants;
        private List<GameObject> _plants;
        
        private IEnumerator _spawningJob;
        private bool _isInstantShow;

        public PlanetMetaController()
        {
            _plants = new List<GameObject>();
        }

        public void SetScore(int score, bool isInstantShow = false)
        {
            _isInstantShow = isInstantShow;
            var compl = score / 100f;
            _actualPlants = (int) (TargetPlants * compl);
        }
        

        private void Update()
        {
            transform.Rotate(0, -Time.deltaTime*RotationSpeed, 0, Space.World);
            
            if (_plants.Count > _actualPlants)
            {
                for (int i = _plants.Count - 1; i >= 0; i--)
                {
                    Destroy(_plants[i]);
                    _plants.RemoveAt(i);
                }
                
            }
            else if (_plants.Count < _actualPlants)
            {
                if (_spawningJob != null)
                    StopCoroutine(_spawningJob);
                _spawningJob = SpawnPlants();
                StartCoroutine(_spawningJob);
            }
        }

        private IEnumerator SpawnPlants()
        {
            var toSpawn = _actualPlants - _plants.Count;
            var portion = _isInstantShow ?_actualPlants : Mathf.CeilToInt(toSpawn / (SpawningTime * Application.targetFrameRate));
//            var iterations = Mathf.CeilToInt(toSpawn / (float)portion);
            while (_plants.Count < _actualPlants)
            {
                for (int i = _plants.Count, j = 0; i < _actualPlants && j < portion; i++ ,j++)
                {
                    _plants.Add(SpawnPlant());
                    
                }
                yield return null;
            }

            _spawningJob = null;
        }

        private GameObject SpawnPlant()
        {
            Vector3 spawnPosition = Random.onUnitSphere * 100 + transform.position;
            var spawnDir = transform.position - spawnPosition;


            RaycastHit hit;
            var isHit = Physics.Raycast(spawnPosition, spawnDir, out hit);
            
            Quaternion spawnRotation = Quaternion.LookRotation(-hit.normal);
            GameObject plant = Instantiate(PlantPrefab, hit.point+hit.normal*0.1f, spawnRotation, transform) as GameObject;
            var val = Random.Range(SizeDiffMin, SizeDiffMax);
          plant.transform.localScale = new Vector3(val, val, val);
            return plant;

        }
    }
}
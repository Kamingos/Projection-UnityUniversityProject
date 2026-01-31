using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Level_1
{
    public class EnemySpawner : MonoBehaviour
    {
        public event Action<GameObject> OnEnemyCreate;

        [SerializeField] private GameObject enemyPrefab;

        [SerializeField] private List<Transform> enemySpawnPointList;

        public void CreateEnemy()
        {
            GameObject _tempObject = Instantiate(enemyPrefab, enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count)].position, Quaternion.identity);

            OnEnemyCreate?.Invoke(_tempObject);
        }

        private void OnDestroy()
        {
            OnEnemyCreate = null;
        }
    }
}

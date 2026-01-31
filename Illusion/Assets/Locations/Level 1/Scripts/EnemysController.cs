using Scripts.Level_1.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Level_1
{
    public class EnemysController : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        [SerializeField] private EnemySpawner enemySpawner;

        [SerializeField] private List<Transform> enemyPatrollingPointsList;

        private GameObject _enemyList;

        private void Awake()
        {
            _enemyList = new();

            enemySpawner.OnEnemyCreate += InitEnemy;
        }

        public void InitEnemy(GameObject enemyObject)
        {
            EnemyAIController _tempController = enemyObject.GetComponent<EnemyAIController>();

            EnemyMovementController _tempMovementController = enemyObject.GetComponent<EnemyMovementController>();

            _tempController.Init(player.transform, enemyPatrollingPointsList);

            _tempMovementController.Init(player);

            _tempController.TurnOn();
        }

        private void OnDestroy()
        {
            enemySpawner.OnEnemyCreate -= InitEnemy;
        }


    }
}

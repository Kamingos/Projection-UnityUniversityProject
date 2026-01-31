using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Scripts.Level_1.Enemy
{
    public enum EnemyState
    {
        Disabled,
        Walking,
        Waring,
        Running,
        Murdering
    }

    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField] private EnemyAnimationController _animationController;

        [SerializeField] private EnemyMovementController _movementController;

        private Transform _playerTransform;

        private Coroutine _currentCoroutine;

        private List<Transform> enemyPointPositions; 

        public void Init(Transform playerTransform, List<Transform> enemyPointPositions)
        {
            _playerTransform = playerTransform;

            this.enemyPointPositions = enemyPointPositions;
        }

        public void TurnOn()
        {
            ChangeState(EnemyState.Walking);
        }

        public void TurnOff()
        {
            ChangeState(EnemyState.Disabled);
        }

        private void ChangeState(EnemyState state)
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            IEnumerator _tempIEnumerator = state switch
            {
                EnemyState.Disabled => DisabledCoroutine(),
                EnemyState.Walking => WalkingCoroutine(),
                EnemyState.Waring => WaringCoroutine(),
                EnemyState.Running => RunningCoroutine(),
                EnemyState.Murdering => MurderingCoroutine(),

                _ => DisabledCoroutine()
            } as IEnumerator;

            _currentCoroutine = StartCoroutine(_tempIEnumerator);
        }

        IEnumerator DisabledCoroutine()
        {
            yield return null;
        }

        IEnumerator WalkingCoroutine()
        {
            while (true)
            {
                Vector3 targetPos = enemyPointPositions[Random.Range(0, enemyPointPositions.Count)].position;

                _movementController.SetDestination(targetPos);

                yield return new WaitUntil(() => Vector3.Distance(transform.position, targetPos) < 1f);
            }

            yield return null;
        }

        IEnumerator WaringCoroutine()
        {
            yield return null;
        }

        IEnumerator RunningCoroutine()
        {
            yield return null;
        }

        IEnumerator MurderingCoroutine()
        {
            yield return null;
        }
    }
}
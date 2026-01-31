using UnityEngine;
using UnityEngine.AI;


namespace Scripts.Level_1.Enemy
{

    public class EnemyMovementController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;

        private GameObject _player;
    
        public void Init(GameObject player)
        {
            _player = player;
        }

        public void SetDestination(Vector3 position)
        {
            agent.SetDestination(position);
        }
        public void SetDestinationToPlayer()
        {
            agent.SetDestination(_player.transform.position);
        }

        public void Stop() => agent.Stop();


    }
}
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private GameObject player;

    private void Awake()
    {

    }

    public void SetDestination(Vector3 position)
    {
        agent.SetDestination(position);
    }
    public void SetDestinationToPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    public void Stop() => agent.Stop();


}

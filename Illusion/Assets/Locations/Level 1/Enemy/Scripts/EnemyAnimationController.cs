using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Level_1.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;

        string last = "";
        string current = "IDLE";

        private void Awake()
        {
            StartCoroutine(cycle());


        }

        IEnumerator cycle()
        {
            WaitForSeconds wfs = new(0.2f);

            while (true)
            {
                if (agent.velocity.magnitude > 0.1f)
                {
                    current = "WALK";
                }
                else
                {
                    current = "IDLE";
                }

                if (current != last)
                {
                    animator.CrossFade(current, 0.2f);

                    last = current;
                }

                yield return wfs;
            }
        }
    }
}
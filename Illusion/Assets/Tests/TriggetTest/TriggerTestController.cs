using UnityEngine;

namespace Scripts.Tests.TriggerTest
{

    public class TriggerTestController : MonoBehaviour
    {
        [SerializeField] private Rigidbody ball;

        [SerializeField] private Transform doorTransform;

        public Transform DoorTransform => doorTransform;

        private void Awake()
        {
            ball.isKinematic = true;
        }

        public void StartSimulation()
        {
            ball.isKinematic = false;
        }

    }
}

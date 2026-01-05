using System.Collections;
using UnityEngine;

namespace Scripts.Hub
{
    public class SinusoidMove : MonoBehaviour
    {
        [SerializeField] private float deltaTime;
        
        [Header("Vertical")]
        [SerializeField] private float verticalDeltaValue;
        [SerializeField] private float verticalKaef;

        [Header("Horizontal")]
        [SerializeField] private float horizontalDeltaValue;
        [SerializeField] private float horizontalKaef;

        void Awake()
        {
            StartCoroutine(MainCycle());
        }

        IEnumerator MainCycle()
        {
            WaitForSeconds wfs = new(deltaTime);

            float _verticalDelta = 0;
            float _horizontalDelta = 0;

            while (true)
            {
                transform.position += Vector3.up * Mathf.Sin(_verticalDelta) * verticalKaef;

                transform.position += Vector3.right * Mathf.Sin(_horizontalDelta) * horizontalKaef;

                _verticalDelta += verticalDeltaValue;
                _horizontalDelta += horizontalDeltaValue;
                yield return wfs;
            }
        }
    }
}
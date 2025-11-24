using UnityEngine;

namespace Scripts.InteractableObjects.Realizations
{
    public class PlayAnimInteractable : MonoBehaviour, IInteractable
    {


        [SerializeField] private Animator _animator;

        [SerializeField] string AnimationName;

        [SerializeField] float duration = 0.1f;

        [SerializeField] bool isPingPong = false;

        private void OnValidate()
        {
            if (duration < 0f) duration = 0f;
        }

        //private void Awake()
        //{
        //    //_animator.speed = -_animator.speed;
        //}

        public void DoAction()
        {
            if (!isPingPong)
                _animator.CrossFade(AnimationName, duration);

            else if (isPingPong)
            { 
                _animator.CrossFade(AnimationName, duration);

                //_animator.speed = -_animator.speed;
            }
        }
    }
}

using UnityEngine;

namespace Scripts.InteractableObjects.Realizations
{
    public class PlayAnimInteractable : MonoBehaviour, IInteractable
    {


        [SerializeField] private Animator _animator;

        [SerializeField] string AnimationName = "ActionAnim";
        [SerializeField] string AnimationBackName = "ActionAnimBack";
        [SerializeField] bool isPingPong = false;

        [SerializeField] bool IsFadeAnimation = false;
        [SerializeField] string AnimationSpeedVariableName = "Direction";

        [SerializeField] float Duration = 0.1f;

        bool isPlayed = false;


        private void OnValidate()
        {
            if (Duration < 0f) Duration = 0f;
        }

        //private void Awake()
        //{
        //    //_animator.speed = -_animator.speed;
        //}

        public void DoAction()
        {
            _animator.StopPlayback();

            if (isPingPong)
            {

                if (!isPlayed)
                {
                    if (IsFadeAnimation) _animator.CrossFade(AnimationName, Duration);
                    if (!IsFadeAnimation) _animator.Play(AnimationName, 0);
                }

                else
                {
                    if (IsFadeAnimation) _animator.CrossFade(AnimationBackName, Duration);
                    if (!IsFadeAnimation) _animator.Play(AnimationBackName, 0);
                }

                isPlayed = !isPlayed;
            }

            else
            {
                if (IsFadeAnimation) _animator.CrossFade(AnimationName, Duration);
                if (!IsFadeAnimation) _animator.Play(AnimationName, 0);
            }
        }
    }
}

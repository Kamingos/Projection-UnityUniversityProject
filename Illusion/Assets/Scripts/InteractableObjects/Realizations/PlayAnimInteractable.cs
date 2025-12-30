using Scripts.SoundManager;
using UnityEngine;

namespace Scripts.InteractableObjects.Realizations
{
    public class PlayAnimInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private Animator animator;

        [SerializeField] private Sound sound;

        [SerializeField] string animationName = "ActionAnim";
        [SerializeField] string animationBackName = "ActionAnimBack";
        [SerializeField] bool isPingPong = false;

        [SerializeField] bool isFadeAnimation = false;
        [SerializeField] string animationSpeedVariableName = "Direction";

        [SerializeField] float duration = 0.1f;

        bool isPlayed = false;


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
            animator.StopPlayback();

            SoundManager.SoundManager.Play(sound, volume: 1);

            if (isPingPong)
            {

                if (!isPlayed)
                {
                    if (isFadeAnimation) animator.CrossFade(animationName, duration);
                    if (!isFadeAnimation) animator.Play(animationName, 0);
                }

                else
                {
                    if (isFadeAnimation) animator.CrossFade(animationBackName, duration);
                    if (!isFadeAnimation) animator.Play(animationBackName, 0);
                }

                isPlayed = !isPlayed;
            }

            else
            {
                if (isFadeAnimation) animator.CrossFade(animationName, duration);
                if (!isFadeAnimation) animator.Play(animationName, 0);
            }
        }
    }
}

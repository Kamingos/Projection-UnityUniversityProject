using UnityEngine;
using Scripts.Player.Controls;
using System.Collections;

public enum AnimState
{
    IDLE,
    WALK,
    TAKE_START,
    TAKE_END,
    JUMP_START,
    JUMP_END
}

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerInputController inputController;
    [SerializeField] private FloorDetector floorDetector;

    [SerializeField] private Animator animator;

    [SerializeField] protected float standartFadeDuration;

    AnimState _current = AnimState.IDLE;
    public AnimState CurrentState { 
        get => _current; 
        set 
        {
            if (CurrentState == value) return;

            _current = value;
        } 
    }

    private Coroutine coroutine;

    private void Awake()
    {
        coroutine = StartCoroutine(AnimationLoop());

        inputController.OnJumpPressed += (_) =>
        {
            CurrentState = AnimState.JUMP_START;
            PlayAnim(CurrentState, standartFadeDuration);
        };
        inputController.OnMouseClick += (_) =>
        {
            CurrentState = AnimState.TAKE_START;
            PlayAnim(CurrentState, standartFadeDuration);
        };

        OnTakeEndScript.OnTakeEnd += () => CurrentState = AnimState.TAKE_END;
    }

    private IEnumerator AnimationLoop()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.1f);

        while (true)
        {
            yield return wfs;

            if (CurrentState == AnimState.TAKE_START) continue;


            if (!floorDetector.IsOnFloor && CurrentState != AnimState.JUMP_START)
            {
                CurrentState = AnimState.JUMP_START;
                PlayAnim(AnimState.JUMP_START, standartFadeDuration);
            }


            if (floorDetector.IsOnFloor && CurrentState == AnimState.JUMP_START)
            {
                CurrentState = AnimState.JUMP_END;
            }

            if(CurrentState == AnimState.JUMP_START) continue;

            //Debug.Log(inputController.MoveDir.magnitude);
            if (inputController.MoveDir.magnitude < 0.1f && CurrentState != AnimState.IDLE)
            {
                CurrentState = AnimState.IDLE;
                PlayAnim(AnimState.IDLE, standartFadeDuration);
            }
            else if (inputController.MoveDir.magnitude >= 0.1f && CurrentState != AnimState.WALK)
            {
                CurrentState = AnimState.WALK;
                PlayAnim(AnimState.WALK, standartFadeDuration);
            }

        }
    }

    private void PlayAnim(AnimState state, float fadeDuration = 0.1f)
    {
        animator.CrossFade(state.ToString(), fadeDuration);
    }
}

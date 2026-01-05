using System;
using UnityEngine;

public class OnTakeEndScript : StateMachineBehaviour
{
    public static event Action OnTakeEnd;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnTakeEnd?.Invoke();
    }
}

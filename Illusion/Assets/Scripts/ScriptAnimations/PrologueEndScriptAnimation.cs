using System.Collections;
using UnityEngine;

public class PrologueEndScriptAnimation : MonoBehaviour
{
    Coroutine coroutine;

    public void PlayAnimation()
    {
        coroutine = StartCoroutine(PlayAnimationCoroutine());
    }

    public IEnumerator PlayAnimationCoroutine()
    {
        yield return null;
    }
}

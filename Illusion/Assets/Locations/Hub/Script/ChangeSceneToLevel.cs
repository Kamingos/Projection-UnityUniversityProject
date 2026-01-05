using GeneralSctipts;
using System.Collections;
using UnityEngine;

public class ChangeSceneToLevel : MonoBehaviour
{
    [SerializeField] private int sceneIndex;

    public void ChangeLevel()
    {
        StartCoroutine(ChangeLevelCoroutinte());
    }

    private IEnumerator ChangeLevelCoroutinte()
    {
        yield return SceneController.LoadSceneCoroutine(sceneIndex);
    }
}

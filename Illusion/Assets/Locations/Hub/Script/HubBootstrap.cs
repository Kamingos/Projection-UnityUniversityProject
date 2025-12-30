using Scripts.SoundManager;
using System.Collections;
using UnityEngine;

public class HubBootstrap : MonoBehaviour
{
    [SerializeField] private DialogSystem dialogSystem;

    private Coroutine _coroutine;

    private void Awake()
    {
        
    }

    private void Start()
    {
        _coroutine = StartCoroutine(StartScene());
    }

    IEnumerator StartScene()
    {
        SoundManager.Play(Sound.HubMusic,volume:0.02f, isLoop: true);

        yield return null;
    }
}

using UnityEngine;
using Scripts.SoundManager;
public class MenuBootstrap : MonoBehaviour
{
    void Start()
    {
        SoundManager.Play(Sound.MenuMusic, isLoop: true);
    }
}

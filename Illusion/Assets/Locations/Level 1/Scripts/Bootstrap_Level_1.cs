using Scripts.SoundManager;
using UnityEngine;

namespace Scripts.Level_1
{
    public class Bootstrap_Level_1 : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        private void Awake()
        {
            SoundManager.SoundManager.Play(Sound.WinterSnowStorm, isLoop: true);
        }
    }
}

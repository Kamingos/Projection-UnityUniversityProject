using Scripts.SoundManager;
using UnityEngine;

namespace Scripts
{
    public class ChangeMusicScript : MonoBehaviour
    {
        [SerializeField] private Sound currentSound = Sound.BasicJump; 
        [SerializeField] private float volume = 0.2f; 
        [SerializeField] private bool isRepeat = false; 

        public void ChangeMusic() => SoundManager.SoundManager.Play(currentSound, volume: volume, isLoop: isRepeat); 
    }
}

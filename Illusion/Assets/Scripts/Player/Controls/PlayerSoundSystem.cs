using Scripts.Player.Controls;
using Scripts.SoundManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSoundSystem : MonoBehaviour
{
    [SerializeField] private PlayerInputController input;
    [SerializeField] private FloorDetector floorDetector;

    private void Awake()
    {
        input.OnJumpPressed += (_) =>
        {

            if (floorDetector.IsOnFloor)
                SoundManager.Play(Sound.BasicJump, volume: 1);
        };

        input.OnMouseClick += (_) => { };
    }
}

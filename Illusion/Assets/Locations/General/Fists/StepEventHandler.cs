using Scripts.SoundManager;
using UnityEngine;
using Scripts.SoundManager;

public class StepEventHandler : MonoBehaviour
{
    [SerializeField] private FloorDetector floorDetector;
    public void PlaySound()
    {
        switch (floorDetector.FloorType)
        {
            case Scripts.FloorType.Wood:
                SoundManager.Play(Sound.WoodFootStep, volume: 0.5f);
                break;
            case Scripts.FloorType.Concrete:
                SoundManager.Play(Sound.ConcreteFootStep, volume: 0.5f);
                break;
            case Scripts.FloorType.Grass:
                SoundManager.Play(Sound.GrassFootStep, volume: 0.5f);
                break;
        }
    }
}

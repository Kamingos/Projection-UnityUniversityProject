using Scripts.SoundManager;
using UnityEngine;

namespace Scripts.Level_1
{
    public class Bootstrap_Level_1 : MonoBehaviour
    {
        [SerializeField] private EnemySpawner spawner;

        [SerializeField] private EnemysController enemysController;

        private void Start()
        {
            spawner.CreateEnemy();
            spawner.CreateEnemy();

            SoundManager.SoundManager.Play(Sound.WinterSnowStorm, isLoop: true);
        }
    }
}
